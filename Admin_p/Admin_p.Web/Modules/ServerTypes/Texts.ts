import { proxyTexts } from "@serenity-is/corelib";

namespace texts {

    export declare namespace Db {

        namespace Administration {

            namespace Language {
                export const LanguageId: string;
                export const LanguageName: string;
            }

            namespace Role {
                export const RoleId: string;
                export const RoleName: string;
            }

            namespace RolePermission {
                export const PermissionKey: string;
                export const RoleId: string;
                export const RoleName: string;
                export const RolePermissionId: string;
            }

            namespace User {
                export const DisplayName: string;
                export const Email: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const LastDirectoryUpdate: string;
                export const Password: string;
                export const PasswordConfirm: string;
                export const PasswordHash: string;
                export const PasswordSalt: string;
                export const Roles: string;
                export const Source: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
                export const UserId: string;
                export const UserImage: string;
                export const Username: string;
            }

            namespace UserPermission {
                export const Granted: string;
                export const PermissionKey: string;
                export const User: string;
                export const UserId: string;
                export const UserPermissionId: string;
                export const Username: string;
            }

            namespace UserRole {
                export const RoleId: string;
                export const RoleName: string;
                export const User: string;
                export const UserId: string;
                export const UserRoleId: string;
                export const Username: string;
            }
        }

        namespace Electro {

            namespace Products {
                export const CategoryId: string;
                export const CategoryName: string;
                export const CreatedAt: string;
                export const Description: string;
                export const ImagePath: string;
                export const Name: string;
                export const Price: string;
                export const ProductId: string;
                export const StockQuantity: string;
                export const UpdatedAt: string;
            }
        }

        namespace electroDB {

            namespace Categories {
                export const CategoryId: string;
                export const CreatedAt: string;
                export const Description: string;
                export const ImageUrl: string;
                export const Name: string;
                export const UpdatedAt: string;
            }

            namespace OrderDetails {
                export const CreatedAt: string;
                export const OrderDetailId: string;
                export const OrderId: string;
                export const OrderStatus: string;
                export const ProductId: string;
                export const ProductName: string;
                export const Quantity: string;
                export const Subtotal: string;
                export const UnitPrice: string;
                export const UpdatedAt: string;
            }

            namespace Orders {
                export const CreatedAt: string;
                export const OrderDate: string;
                export const OrderId: string;
                export const Status: string;
                export const TotalAmount: string;
                export const UpdatedAt: string;
                export const UserId: string;
                export const UserName: string;
            }

            namespace Payments {
                export const Amount: string;
                export const CreatedAt: string;
                export const OrderId: string;
                export const OrderStatus: string;
                export const PaymentId: string;
                export const PaymentMethod: string;
                export const PaymentStatus: string;
                export const TransactionDate: string;
                export const TransactionId: string;
                export const UpdatedAt: string;
            }

            namespace Products {
                export const CategoryId: string;
                export const CategoryName: string;
                export const CreatedAt: string;
                export const Description: string;
                export const ImagePath: string;
                export const Name: string;
                export const Price: string;
                export const ProductId: string;
                export const StockQuantity: string;
                export const UpdatedAt: string;
            }

            namespace PromotionProducts {
                export const CreatedAt: string;
                export const ProductId: string;
                export const ProductName: string;
                export const PromotionId: string;
                export const PromotionName: string;
                export const PromotionProductId: string;
                export const UpdatedAt: string;
            }

            namespace Promotions {
                export const CreatedAt: string;
                export const Description: string;
                export const DiscountPercentage: string;
                export const EndDate: string;
                export const IsActive: string;
                export const Name: string;
                export const PromotionId: string;
                export const StartDate: string;
                export const UpdatedAt: string;
            }

            namespace ShoppingCarts {
                export const CartId: string;
                export const CreatedAt: string;
                export const ProductId: string;
                export const ProductName: string;
                export const Quantity: string;
                export const UpdatedAt: string;
                export const UserId: string;
                export const UserName: string;
            }

            namespace Users {
                export const CreatedAt: string;
                export const Email: string;
                export const PasswordHash: string;
                export const PhoneNumber: string;
                export const Role: string;
                export const ShippingAddress: string;
                export const UpdatedAt: string;
                export const UserId: string;
                export const UserName: string;
            }
        }
    }

    export declare namespace Forms {

        namespace Membership {

            namespace Login {
                export const ForgotPassword: string;
                export const LoginToYourAccount: string;
                export const RememberMe: string;
                export const SignInButton: string;
                export const SignUpButton: string;
            }

            namespace SignUp {
                export const ActivateEmailSubject: string;
                export const ActivationCompleteMessage: string;
                export const ConfirmEmail: string;
                export const ConfirmPassword: string;
                export const DisplayName: string;
                export const Email: string;
                export const FormInfo: string;
                export const FormTitle: string;
                export const Password: string;
                export const SubmitButton: string;
                export const Success: string;
            }
        }
        export const SiteTitle: string;
    }

    export declare namespace Site {

        namespace AccessDenied {
            export const ClickToChangeUser: string;
            export const ClickToLogin: string;
            export const LackPermissions: string;
            export const NotLoggedIn: string;
            export const PageTitle: string;
        }

        namespace Layout {
            export const Language: string;
            export const Theme: string;
        }

        namespace RolePermissionDialog {
            export const DialogTitle: string;
            export const EditButton: string;
            export const SaveSuccess: string;
        }

        namespace UserDialog {
            export const EditPermissionsButton: string;
            export const EditRolesButton: string;
        }

        namespace UserPermissionDialog {
            export const DialogTitle: string;
            export const Grant: string;
            export const Permission: string;
            export const Revoke: string;
            export const SaveSuccess: string;
        }

        namespace ValidationError {
            export const Title: string;
        }
    }

    export declare namespace Validation {
        export const AuthenticationError: string;
        export const CurrentPasswordMismatch: string;
        export const DeleteForeignKeyError: string;
        export const EmailConfirm: string;
        export const EmailInUse: string;
        export const InvalidActivateToken: string;
        export const InvalidResetToken: string;
        export const MinRequiredPasswordLength: string;
        export const PasswordConfirmMismatch: string;
        export const SavePrimaryKeyError: string;
    }

}

const Texts: typeof texts = proxyTexts({}, '', {
    Db: {
        Administration: {
            Language: {},
            Role: {},
            RolePermission: {},
            User: {},
            UserPermission: {},
            UserRole: {}
        },
        Electro: {
            Products: {}
        },
        electroDB: {
            Categories: {},
            OrderDetails: {},
            Orders: {},
            Payments: {},
            Products: {},
            PromotionProducts: {},
            Promotions: {},
            ShoppingCarts: {},
            Users: {}
        }
    },
    Forms: {
        Membership: {
            Login: {},
            SignUp: {}
        }
    },
    Site: {
        AccessDenied: {},
        Layout: {},
        RolePermissionDialog: {},
        UserDialog: {},
        UserPermissionDialog: {},
        ValidationError: {}
    },
    Validation: {}
}) as any;

export const AccessDeniedViewTexts = Texts.Site.AccessDenied;

export const LoginFormTexts = Texts.Forms.Membership.Login;

export const MembershipValidationTexts = Texts.Validation;

export const RolePermissionDialogTexts = Texts.Site.RolePermissionDialog;

export const SignUpFormTexts = Texts.Forms.Membership.SignUp;

export const SiteFormTexts = Texts.Forms;

export const SiteLayoutTexts = Texts.Site.Layout;

export const SqlExceptionHelperTexts = Texts.Validation;

export const UserDialogTexts = Texts.Site.UserDialog;

export const UserPermissionDialogTexts = Texts.Site.UserPermissionDialog;

export const ValidationErrorViewTexts = Texts.Site.ValidationError;