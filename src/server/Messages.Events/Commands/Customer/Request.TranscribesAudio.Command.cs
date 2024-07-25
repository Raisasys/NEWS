using Core;
using Domain;
using FluentValidation;

namespace Commands.Customer;

public class TranscribesAudioRequestCommand : CustomerCommandBase
{
    public Stream Audio { get; set; }
    public int Duration { get; set; }
}


public class TranscribesAudioRequestCommandValidator : AbstractValidator<TranscribesAudioRequestCommand>
{
    public TranscribesAudioRequestCommandValidator()
    {
        RuleFor(x => x.Audio)
            .NotNull()
            .NotEmpty();
    }
}