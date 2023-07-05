namespace TCG.Common.MassTransit.Events;

public record FidelityPointFailedEvent(Guid CorrelationId,string ErrorMessage, int UserId);