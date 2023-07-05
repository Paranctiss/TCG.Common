namespace TCG.Common.MassTransit.Messages;

public record AddFidelityPointMessage(Guid CorrelationId, int Point, int UserId);