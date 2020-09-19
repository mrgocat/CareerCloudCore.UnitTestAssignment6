"# CareerCloudCore.UnitTestAssignment6" 

1. CareerCloud.gRPC project should be run before run test mehtod. 

2. [Address_gRPC_HOST] value to be changed to the same as gRPC host.

const string Address_gRPC_HOST = "https://localhost:5002"; // the host address where gRPC service is on

3. In CareerCloud.gRPC project students must make Mapper static class like below.

ProtoMapper.cs 
************************
<pre>
namespace CareerCloud.gRPC.Services
{
    public static class ProtoMapper
    {
        public static SecurityLoginProto mapFromSecurityLoginPoco(SecurityLoginPoco poco)
        {
            return new SecurityLoginProto()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login,
                Password = poco.Password,
                Created = ProtoMapper.convertDateTime2TimeStamp(poco.Created),
                PasswordUpdate = ProtoMapper.convertDateTime2TimeStamp(poco.PasswordUpdate),
                AgreementAccepted = ProtoMapper.convertDateTime2TimeStamp(poco.AgreementAccepted),
                IsLocked = poco.IsLocked,
                IsInactive = poco.IsInactive,
                EmailAddress = poco.EmailAddress,
                PhoneNumber = poco.PhoneNumber == null ? string.Empty : poco.PhoneNumber,
                FullName = poco.FullName,
                ForceChangePassword = poco.ForceChangePassword,
                PrefferredLanguage = poco.PrefferredLanguage == null ? string.Empty : poco.PrefferredLanguage,
                TimeStamp = poco.TimeStamp == null ? ByteString.Empty : ByteString.CopyFrom(poco.TimeStamp)
            };
        }
        public static SecurityLoginPoco mapToSecurityLoginPoco(SecurityLoginProto reply)
        {
            return new SecurityLoginPoco()
            {
                Id = Guid.Parse(reply.Id),
                Login = reply.Login,
                Password = reply.Password,
                Created = reply.Created.ToDateTime(),
                PasswordUpdate = reply.PasswordUpdate.ToDateTime(),
                AgreementAccepted = reply.AgreementAccepted.ToDateTime(),
                IsLocked = reply.IsLocked,
                IsInactive = reply.IsInactive,
                EmailAddress = reply.EmailAddress,
                PhoneNumber = reply.PhoneNumber,
                FullName = reply.FullName,
                ForceChangePassword = reply.ForceChangePassword,
                PrefferredLanguage = reply.PrefferredLanguage,
                TimeStamp = reply.TimeStamp.ToArray()
            };
        }
    }
}
</pre>
