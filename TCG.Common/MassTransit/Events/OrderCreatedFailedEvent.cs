namespace TCG.Common.MassTransit.Events;

public record OrderCreatedFailedEvent(Guid PostId, char Status, string ErrorMessage);
