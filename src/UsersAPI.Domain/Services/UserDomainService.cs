﻿using UsersAPI.Domain.Exceptions;
using UsersAPI.Domain.Interfaces.Messages;
using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Domain.Interfaces.Services;
using UsersAPI.Domain.Models;
using UsersAPI.Domain.ValueObjects;

namespace UsersAPI.Domain.Services;

public class UserDomainService : IUserDomainService
{
    private readonly IUnitOfWork? _unitOfWork;
    private readonly IUserMessageProducer? _userMessageProducer;

    public UserDomainService(IUnitOfWork? unitOfWork, IUserMessageProducer? userMessageProducer)
    {
        _unitOfWork = unitOfWork;
        _userMessageProducer = userMessageProducer;
    }

    public void Add(User user)
    {
        if (Get(user.Email) != null)
            throw new EmailAlreadyExistsException(user.Email);

        _unitOfWork?.UserRepository.Add(user);
        _unitOfWork?.SaveChanges();

        _userMessageProducer?.Send(new UserMessageVO
        {
            Id = user.Id,
            SendedAt = DateTime.Now,
            To = user.Email,
            Subject = "Parabéns, sua conta de usuário foi criada com sucesso!",
            Body = @$"Olá {user.Name}, seu cadastro foi realizado com sucesso em nosso sistema."
        });
    }

    public void Update(User user)
    {
        _unitOfWork?.UserRepository.Update(user);
        _unitOfWork?.SaveChanges();
    }

    public void Delete(User user)
    {
        _unitOfWork?.UserRepository.Delete(user);
        _unitOfWork?.SaveChanges();
    }

    public User? Get(Guid id)
    {
        return _unitOfWork?.UserRepository.GetById(id);
    }

    public User? Get(string email)
    {
        return _unitOfWork?.UserRepository.Get(u => u.Email.Equals(email));
    }

    public User? Get(string email, string password)
    {
        return _unitOfWork?.UserRepository.Get(u => u.Email.Equals(email) && u.Password.Equals(password));
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}