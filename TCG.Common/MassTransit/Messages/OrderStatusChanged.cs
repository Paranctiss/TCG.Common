namespace TCG.Common.MassTransit.Messages;

public record OrderStatusChanged(int OrderId, char OrderStatus);
