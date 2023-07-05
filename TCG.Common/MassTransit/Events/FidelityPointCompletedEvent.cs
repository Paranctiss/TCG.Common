namespace TCG.Common.MassTransit.Events;

public record FidelityPointCompletedEvent(Guid CorrelationId);