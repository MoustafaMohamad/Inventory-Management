using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Dto;
using MediatR;

namespace Inventory_Management.Features.Common.Users.Queries
{
    public record GetUserByEmailQuery(string Email) : IRequest<ResultDto<UserDto>>;
    public class GetUserByEmailQueryHandler : BaseRequestHandler<User, GetUserByEmailQuery, ResultDto<UserDto>>
    {
        public GetUserByEmailQueryHandler( RequestParameters<User> requestParameters) : base(requestParameters)
        {

        }

        public async override Task<ResultDto<UserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.FirstAsync(u => u.Email == request.Email);
            if (user is null) 
            {
                return ResultDto<UserDto>.Faliure(ErrorCode.EmailIsNotFound, "Email is not Found");
            }
            return ResultDto<UserDto>.Sucess(user.MapOne<UserDto>());
        }
    }
}
