using LearnEase.Core.Entities;
using LearnEase.Core.Enum;
using LearnEase.Repository.UOW;
using LearnEase_Api.Dtos.reponse;
using LearnEase_Api.Dtos.request;
using LearnEase_Api.LearnEase.Core.IServices;
using LearnEase_Api.LearnEase.Infrastructure.IRepository;
using LearnEase_Api.Mapper;
using Microsoft.Extensions.Logging;

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

			var nameParts = request.userName?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
			string firstName = nameParts.FirstOrDefault() ?? string.Empty;
			string lastName = nameParts.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : string.Empty;

			var user = new User
			{
				UserId = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"),
				IsActive = true,
				Email = request.email,
				UserName = request.userName,
				Role = UserRoleEnum.User,
				FirstName = firstName,
				LastName = lastName,
				ImageUrl = request.urlImage,
			};

			await _unitOfWork.GetCustomRepository<IUserRepository>().CreateAsync(user);
			await _unitOfWork.SaveAsync();

			return _mapper.MapperUserReponse(user);
		}

		public async Task<UserReponse> DeleteUserReponseById(string id)
		{
			var userRepo = _unitOfWork.GetRepository<User>();
			var user = await userRepo.GetByIdAsync(id);
			if (user == null) return null;

			await userRepo.DeleteAsync(user);
			await _unitOfWork.SaveAsync();

			return _mapper.MapperUserReponse(user);
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

			var userRepo = _unitOfWork.GetRepository<User>();
			var findUser = await userRepo.GetByIdAsync(id);
			if (findUser == null)
				return null;

			findUser.Email = request.email;
			findUser.UserName = request.userName;
			findUser.IsActive = findUser.IsActive;

			await userRepo.UpdateAsync(findUser);
			await _unitOfWork.SaveAsync();

			return _mapper.MapperUserReponse(findUser);
		}
	}
}
