﻿using LearnEase.Repository.UOW;
using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.Entity;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;
using LearnEase_Api.Mapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LearnEase_Api.LearnEase.Core.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<UserService> _logger;
		private readonly MapperUser _mapper = new MapperUser();

		public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<UserReponse> CreateNewUser(UserCreationRequest request)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			var findUser = await _unitOfWork.GetCustomRepository<IUserRepository>().FindByEmail(request.email);
			if (findUser != null) return null;

			var user = new User
			{
				UserId = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"),
				UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"),
				IsActive = true,
				Email = request.email,
				UserName = request.userName,
				Role = "User" 
			};

			await _unitOfWork.GetRepository<User>().CreateAsync(user);
			await _unitOfWork.SaveAsync();

			var userDetail = new UserDetail
			{
				FirstName = request.userName.Split(' ')[0],
				LastName = string.Join(" ", request.userName.Split(' ').Skip(1)),
				ImageUrl = request.urlImage,
				CreatedAt = user.CreatedAt,
				UpdatedAt = user.UpdatedAt,
				UserId = user.UserId
			};

			await _unitOfWork.GetCustomRepository<IUserDetailRepository>().CreateAsync(userDetail);
			await _unitOfWork.SaveAsync();

			return _mapper.MapperUserReponse(user);
		}

		public async Task<UserReponse> DeleteUserReponseById(string id)
		{
			var userRepo = _unitOfWork.GetRepository<User>();
			var userDetailRepo = _unitOfWork.GetRepository<UserDetail>();

			var findUserById = await userRepo.GetByIdAsync(id);
			if (findUserById == null) return null;

			var userResponse = _mapper.MapperUserReponse(findUserById);

			var userDetail = await userDetailRepo.GetByIdAsync(id);
			if (userDetail != null)
			{
				await userDetailRepo.DeleteAsync(userDetail);
			}

			await userRepo.DeleteAsync(findUserById);
			await _unitOfWork.SaveAsync();

			return userResponse;
		}

		public async Task<UserReponse> FindUserByEmail(string email)
		{
			var result = await _unitOfWork.GetCustomRepository<IUserRepository>().FindByEmail(email);
			return result != null ? _mapper.MapperUserReponse(result) : null;
		}

		public async Task<UserReponse> GetUserReponseById(string id)
		{
			var result = await _unitOfWork.GetRepository<User>().GetByIdAsync(id);
			return result != null ? _mapper.MapperUserReponse(result) : null;
		}

		public async Task<UserReponse> UpdateUserReponse(UserUpdateRequest request, string id)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			var findUser = await _unitOfWork.GetRepository<User>().GetByIdAsync(id);
			if (findUser == null) return null;

			findUser.UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
			findUser.Email = request.email;
			findUser.UserName = request.userName;

			await _unitOfWork.GetRepository<User>().UpdateAsync(findUser);
			await _unitOfWork.SaveAsync();

			return _mapper.MapperUserReponse(findUser);
		}
	}
}
