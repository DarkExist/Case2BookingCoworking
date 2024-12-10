using Case2BookingCoworking.Application.Abstract.Email;
using Case2BookingCoworking.Application.Abstract;
using Case2BookingCoworking.Application.Abstract.Services;
using Case2BookingCoworking.Contracts.Requests;
using ErrorOr;
using Case2BookingCoworking.Application.Abstract.Repos;
using Case2BookingCoworking.Core.Domain.Entities;
using System.Data;

namespace Case2BookingCoworking.Services
{
    public class UserService : IUserService
    {
       // private List<string> defaultRoles = new List<string> { "USER" };
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepos _userRepository;
        private readonly IRoleRepos _roleRepository;
        private readonly IJWTProvider _jwtProvider;
        private readonly IEmailVerificationService _emailVerificationService;
        public UserService(IPasswordHasher passwordHasher, IUserRepos userRepository, IRoleRepos roleRepository, IJWTProvider jwtProvider, IEmailVerificationService emailVerificationService)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _jwtProvider = jwtProvider;
            _emailVerificationService = emailVerificationService;
        }
        private async Task<ErrorOr<List<Role>>> GetUserRolesAsync(CancellationToken cancellationToken, List<string> RequiredRoles)
        {
            var allroles = await _roleRepository.GetAllAsync(cancellationToken);
            var roles = allroles.Value.Where(r=> RequiredRoles.Contains(r.Name));

            foreach (var role in roles) { await _roleRepository.Attach(role, cancellationToken); }

            return roles.ToList();
        }
        private async Task<ErrorOr<User>> CreateUserAsync(

            UserRegisterRequest userRegisterRequest,
            CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.Generate(userRegisterRequest.Password);

            var userRoles = await GetUserRolesAsync(cancellationToken, userRegisterRequest.Roles);
            var user = new User()
            {
                Name = userRegisterRequest.Name,
                Email = userRegisterRequest.Email,
                Password = hashedPassword,
                Roles = userRoles.Value,
                Orders = new List<Order>()
            };
            var userProfile = new Profile() { 
                User = user,
                UserId = user.Id
            };
            user.Profile = userProfile;


            return user;
        }

        public Task<ErrorOr<string>> Login(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<Success>> RegisterIfVerifiedAsync(UserRegisterRequest userRegistrationRequest, string code, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ErrorOr<Success>> SendEmailVerificationCode(string email, CancellationToken cancellationToken)
        {
            return await _emailVerificationService.SendRegistrationCodeMessageAsync(email, cancellationToken);
        }
    }
}
