"# CareerCloudCore.UnitTestAssignment6" 

1. CareerCloud.gRPC project should be run before run test mehtod. 

2. [Address_gRPC_HOST] value to be changed to the same as gRPC host.

const string Address_gRPC_HOST = "https://localhost:5002"; // the host address where gRPC service is on

3. The GrpService attribut of Protbuf element for each poco in [CareerCloud.gRPC.csproj] file should be "Server, Client" as below.

&lt;ItemGroup&gt;<br/>
	&nbsp; &nbsp; &lt;Protobuf Include="Protos\SystemLanguageCode.proto" GrpcServices="Server,Client" /&gt;<br/>
&lt;/ItemGroup&gt;

4. In CareerCloud.gRPC project students must make Mapper static class like below.

ProtoMapper.cs 
************************
<pre>
namespace CareerCloud.gRPC.Services
{
    public static class ProtoMapper
    {
        public static SecurityLoginProto MapFromSecurityLoginPoco(SecurityLoginPoco poco)
        {
            return new SecurityLoginProto()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login,
                Password = poco.Password,
                Created = ProtoMapper.ConvertDateTime2TimeStamp(poco.Created),
                PasswordUpdate = ProtoMapper.ConvertDateTime2TimeStamp(poco.PasswordUpdate),
                AgreementAccepted = ProtoMapper.ConvertDateTime2TimeStamp(poco.AgreementAccepted),
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
        public static SecurityLoginPoco MapToSecurityLoginPoco(SecurityLoginProto reply)
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
