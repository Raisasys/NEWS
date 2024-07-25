using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Xml.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Core;

public interface IApiBaseResponse
{
    
}
public interface IBaseHandler
{
    Task<IApiBaseResponse> Handle();
}

public abstract class BaseHandler<TCommand> : IBaseHandler
    where TCommand : CommandBase
{
    protected IUowDatabase Database => Context.GetDatabase();
    protected IValidator<TCommand> Validator => Context.Current.GetService<IValidator<TCommand>>();
    public ClaimsIdentity ClaimsIdentity { get; set; }
    protected TCommand Command { get; set; }

    protected abstract Task<ValidationResult> ValidateRequest(TCommand command);
    public abstract Task<IApiBaseResponse> Handle();


    public async Task<IApiBaseResponse> Execute(TCommand command)
    {
        var validationResponse = await ValidateRequest(command);
        /*if (!validationResponse.IsValid)
            return new ApiErrorResponse(ErrorResponseType.InvalidRequest, validationResponse.Errors.Select(i=>i.ErrorMessage).ToList());
            */

        Command = command;
        return await Handle();
    }


    /*protected ApiSuccessTResponse<TData> Success<TData>(TData result, string msg = null) => new ApiSuccessTResponse<TData>(result,msg);
    protected ApiSuccessResponse Success(string msg = null) => new ApiSuccessResponse(msg);

	protected ApiFailedResponse Failed(FailedResponseType failedType, NextActionOnFailedType nextAction, string message=null) => new ApiFailedResponse(failedType, nextAction, message ==null ? null : new List<string>{ message });
    protected ApiFailedResponse Failed(NextActionOnFailedType nextAction, string message = null) => Failed(FailedResponseType.FailedAndDisplayMessageDoNextAction,nextAction,message);
	protected ApiFailedResponse Failed(string message) => Failed(FailedResponseType.FailedAndDisplayMessageDoNextAction, NextActionOnFailedType.Nothing, message);

	protected ApiFailedResponse Failed(FailedResponseType failedType, NextActionOnFailedType nextAction, List<string> messages) => new ApiFailedResponse(failedType, nextAction, messages);
	protected ApiFailedResponse Failed(NextActionOnFailedType nextAction, List<string> messages) => Failed(FailedResponseType.FailedAndDisplayMessageDoNextAction, nextAction, messages);
	protected ApiFailedResponse Failed(List<string> messages) => Failed(FailedResponseType.FailedAndDisplayMessageDoNextAction, NextActionOnFailedType.Nothing, messages);

	protected ApiFailedTResponse<TData> Failed<TData>(FailedResponseType failedType, NextActionOnFailedType nextAction, string message = null) => new ApiFailedTResponse<TData>(failedType, nextAction, message == null ? null : new List<string> { message });
	protected ApiFailedTResponse<TData> Failed<TData>(NextActionOnFailedType nextAction, string message = null) => Failed<TData>(FailedResponseType.FailedAndDisplayMessageDoNextAction, nextAction, message);
	protected ApiFailedTResponse<TData> Failed<TData>(string message) => Failed<TData>(FailedResponseType.FailedAndDisplayMessageDoNextAction, NextActionOnFailedType.Nothing, message);

	protected ApiFailedTResponse<TData> Failed<TData>(FailedResponseType failedType, NextActionOnFailedType nextAction, List<string> messages) => new ApiFailedTResponse<TData>(failedType, nextAction, messages);
	protected ApiFailedTResponse<TData> Failed<TData>(NextActionOnFailedType nextAction, List<string> messages) => Failed<TData>(FailedResponseType.FailedAndDisplayMessageDoNextAction, nextAction, messages);
	protected ApiFailedTResponse<TData> Failed<TData>(List<string> messages) => Failed<TData>(FailedResponseType.FailedAndDisplayMessageDoNextAction, NextActionOnFailedType.Nothing, messages);


	protected ApiFailedResponse Failed(ServiceFailedDetails failedDetails) => new ApiFailedResponse(failedDetails.FailedType, NextActionOnFailedType.Nothing,new List<string>{ failedDetails.ErrorMessage});
	protected ApiFailedResponse Failed(ServiceFailedDetails failedDetails,NextActionOnFailedType nextAction) => new ApiFailedResponse(failedDetails.FailedType, nextAction, new List<string> { failedDetails.ErrorMessage });

	protected ApiFailedTResponse<TData> Failed<TData>(ServiceFailedDetails failedDetails) => new ApiFailedTResponse<TData>(failedDetails.FailedType, NextActionOnFailedType.Nothing, new List<string> { failedDetails.ErrorMessage });
	protected ApiFailedTResponse<TData> Failed<TData>(ServiceFailedDetails failedDetails, NextActionOnFailedType nextAction) => new ApiFailedTResponse<TData>(failedDetails.FailedType, nextAction, new List<string> { failedDetails.ErrorMessage });*/
}
