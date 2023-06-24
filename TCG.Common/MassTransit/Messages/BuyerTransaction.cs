namespace TCG.Common.MassTransit.Messages;

public record BuyerTransaction(string MerchId, int buyerId);