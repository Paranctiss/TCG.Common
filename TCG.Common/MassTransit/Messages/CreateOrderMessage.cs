namespace TCG.Common.MassTransit.Messages;

public record CreateOrderMessage(int OrderId, Guid PostId, char OrderStatus, int UserId);