using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.Users.RegisterUser.Queries
{
    public record IsEmailExistQuery(string email) : IRequest<ResultDto<bool>>;
    public class IsEmailExistQueryHandler  : BaseRequestHandler<User, IsEmailExistQuery, ResultDto<bool>>
    {
        public IsEmailExistQueryHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<ResultDto<bool>> Handle(IsEmailExistQuery request, CancellationToken cancellationToken)
        {
            var result = await Task.Run(() => _repository.AnyAsync(u => u.Email == request.email));
            if (result)
            {
                return ResultDto<bool>.Sucess(true);
            }
            return ResultDto<bool>.Faliure(ErrorCode.EmailIsNotFound, "Email is Not Found");
        }

    }
}
