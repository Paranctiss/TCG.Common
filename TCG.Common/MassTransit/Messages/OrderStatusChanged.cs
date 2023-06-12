namespace TCG.Common.MassTransit.Messages;

public record OrderStatusChanged(Guid PostId, char OrderStatus);
