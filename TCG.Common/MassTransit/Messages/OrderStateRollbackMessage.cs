namespace TCG.Common.MassTransit.Messages;

public record OrderStateRollbackMessage(Guid PostId);