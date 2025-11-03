namespace WebBuySource.Models.Enums
{
    public enum Gender { MALE, FEMALE, OTHER, Male}
    public enum UserStatus { ACTIVE, INACTIVE, BANNED }
    public enum TransactionStatus { PENDING, COMPLETED, FAILED, REFUNDED }
    public enum PayoutStatus { PENDING, PAID, FAILED }
    public enum PromotionType { PERCENTAGE, FIXED_AMOUNT }
    public enum PromotionStatus { ACTIVE, INACTIVE, EXPIRED }
    public enum NotificationType { SYSTEM, TRANSACTION, MESSAGE }
    public enum NotificationStatus { UNREAD, READ }
    public enum LogAction { CREATE, UPDATE, DELETE, LOGIN, LOGOUT }
    public enum ReportReason { SPAM, ABUSE, COPYRIGHT, OTHER }
    public enum HTTPMethod { GET, POST, PUT, DELETE, PATCH }

    public enum PromotionDiscountType { PERCENT, FIXED }

    public enum VerificationCodeType { RESET_PASSWORD = 2, REGISTER= 1 }

    public enum ConversationStatus { ACTIVE, INACTIVE, EXPIRED }

    public enum TransactionType
    {
        PENDING, COMPLETED, FAILED,
    }
}