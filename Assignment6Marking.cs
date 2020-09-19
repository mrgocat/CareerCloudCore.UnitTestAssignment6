
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using CareerCloud.gRPC.Services;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using CareerCloud.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloudCore.UnitTests.Assignment6
{
    [TestClass]
    public class Assignment6Marking
    {
        const string Address_gRPC_HOST = "https://localhost:5002"; // the host address where gRPC service is on
        private GrpcChannel _channel;

        private SystemCountryCodePoco _systemCountry;
        private ApplicantEducationPoco _applicantEducation;
        private ApplicantProfilePoco _applicantProfile;
        private ApplicantJobApplicationPoco _applicantJobApplication;
        private CompanyJobPoco _companyJob;
        private CompanyProfilePoco _companyProfile;
        private CompanyDescriptionPoco _companyDescription;
        private SystemLanguageCodePoco _systemLangCode;
        private ApplicantResumePoco _applicantResume;
        private ApplicantSkillPoco _applicantSkills;
        private ApplicantWorkHistoryPoco _appliantWorkHistory;
        private CompanyJobEducationPoco _companyJobEducation;
        private CompanyJobSkillPoco _companyJobSkill;
        private CompanyJobDescriptionPoco _companyJobDescription;
        private CompanyLocationPoco _companyLocation;
        private SecurityLoginPoco _securityLogin;
        private SecurityLoginsLogPoco _securityLoginLog;
        private SecurityRolePoco _securityRole;
        private SecurityLoginsRolePoco _securityLoginRole;

        [TestInitialize]
        public void Initialize()
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            _channel = GrpcChannel.ForAddress(Address_gRPC_HOST,
                new GrpcChannelOptions { HttpHandler = httpHandler });

            Init_Pocos();
        }


        public void Init_Pocos()
        {
            SystemCountry_Init();
            CompanyProfile_Init();
            SystemLangCode_Init();
            CompanyDescription_Init();
            CompanyJob_Init();
            CompanyJobEducation_Init();
            CompanyJobSkill_Init();
            CompanyJobDescription_Init();
            CompanyLocation_Init();
            SecurityLogin_Init();
            ApplicantProfile_Init();
            SecurityLoginLog_Init();
            SecurityRole_Init();
            SecurityLoginRole_Init();
            ApplicantEducation_Init();
            ApplicantResume_Init();
            ApplicantSkills_Init();
            AappliantWorkHistory_Init();
            ApplicantJobApplication_Init();

        }

        #region PocoInitialization
        private void ApplicantJobApplication_Init()
        {
            _applicantJobApplication = new ApplicantJobApplicationPoco()
            {
                Id = Guid.NewGuid(),
                ApplicationDate = Faker.Date.Recent(),
                Applicant = _applicantProfile.Id,
                Job = _companyJob.Id
            };
        }

        private void AappliantWorkHistory_Init()
        {
            _appliantWorkHistory = new ApplicantWorkHistoryPoco()
            {
                Id = Guid.NewGuid(),
                Applicant = _applicantProfile.Id,
                CompanyName = Truncate(Faker.Lorem.Sentence(), 150),
                CountryCode = _systemCountry.Code,
                EndMonth = 12,
                EndYear = 1999,
                JobDescription = Truncate(Faker.Lorem.Sentence(), 500),
                JobTitle = Truncate(Faker.Lorem.Sentence(), 50),
                Location = Faker.Address.StreetName(),
                StartMonth = 1,
                StartYear = 1999
            };
        }

        private void ApplicantSkills_Init()
        {
            _applicantSkills = new ApplicantSkillPoco()
            {
                Applicant = _applicantProfile.Id,
                Id = Guid.NewGuid(),
                EndMonth = 12,
                EndYear = 1999,
                Skill = Truncate(Faker.Lorem.Sentence(), 100),
                SkillLevel = Truncate(Faker.Lorem.Sentence(), 10),
                StartMonth = 1,
                StartYear = 1999
            };
        }

        private void ApplicantResume_Init()
        {
            _applicantResume = new ApplicantResumePoco()
            {
                Applicant = _applicantProfile.Id,
                Id = Guid.NewGuid(),
                Resume = Faker.Lorem.Paragraph(),
                LastUpdated = Faker.Date.Recent()
            };
        }

        private void ApplicantEducation_Init()
        {
            _applicantEducation = new ApplicantEducationPoco()
            {
                Id = Guid.NewGuid(),
                Applicant = _applicantProfile.Id,
                Major = Faker.Education.Major(),
                CertificateDiploma = Faker.Education.Major(),
                StartDate = Faker.Date.Past(3),
                TimeStamp = new Byte[5],
                CompletionDate = Faker.Date.Forward(1),
                CompletionPercent = (byte)Faker.Number.RandomNumber(1)
            };
        }

        private void SecurityLoginRole_Init()
        {
            _securityLoginRole = new SecurityLoginsRolePoco()
            {
                Id = Guid.NewGuid(),
                Login = _securityLogin.Id,
                Role = _securityRole.Id
            };
        }

        private void SecurityRole_Init()
        {
            _securityRole = new SecurityRolePoco()
            {
                Id = Guid.NewGuid(),
                IsInactive = true,
                Role = Truncate(Faker.Company.Industry(), 50)

            };
        }

        private void SecurityLoginLog_Init()
        {
            _securityLoginLog = new SecurityLoginsLogPoco()
            {
                Id = Guid.NewGuid(),
                IsSuccesful = true,
                Login = _securityLogin.Id,
                LogonDate = Faker.Date.PastWithTime(),
                SourceIP = Faker.Internet.IPv4().PadRight(15)
            };
        }

        private void ApplicantProfile_Init()
        {
            _applicantProfile = new ApplicantProfilePoco
            {
                Id = Guid.NewGuid(),
                Login = _securityLogin.Id,
                City = Faker.Address.CityPrefix(),
                Country = _systemCountry.Code,
                Currency = "CAN".PadRight(10),
                CurrentRate = 71.25M,
                CurrentSalary = 67500,
                Province = Truncate(Faker.Address.Province(), 10).PadRight(10),
                Street = Truncate(Faker.Address.StreetName(), 100),
                PostalCode = Truncate(Faker.Address.CanadianZipCode(), 20).PadRight(20)
            };
        }

        private void SecurityLogin_Init()
        {
            _securityLogin = new SecurityLoginPoco()
            {
                Login = Faker.User.Email(),
                AgreementAccepted = Faker.Date.PastWithTime(),
                Created = Faker.Date.PastWithTime(),
                EmailAddress = Faker.User.Email(),
                ForceChangePassword = false,
                FullName = Faker.Name.FullName(),
                Id = Guid.NewGuid(),
                IsInactive = false,
                IsLocked = false,
                Password = "SoMePassWord#&@",
                PasswordUpdate = Faker.Date.Forward(),
                PhoneNumber = "555-416-9889",
                PrefferredLanguage = "EN".PadRight(10)
            };
        }

        private void CompanyLocation_Init()
        {
            _companyLocation = new CompanyLocationPoco()
            {
                Id = Guid.NewGuid(),
                City = Faker.Address.CityPrefix(),
                Company = _companyProfile.Id,
                CountryCode = _systemCountry.Code,
                Province = Faker.Address.ProvinceAbbreviation(),
                Street = Faker.Address.StreetName(),
                PostalCode = Faker.Address.CanadianZipCode()
            };
        }

        private void CompanyJobDescription_Init()
        {
            _companyJobDescription = new CompanyJobDescriptionPoco()
            {
                Id = Guid.NewGuid(),
                Job = _companyJob.Id,
                JobDescriptions = Truncate(Faker.Lorem.Paragraph(), 999),
                JobName = Truncate(Faker.Lorem.Sentence(), 99)
            };
        }

        private void CompanyJobSkill_Init()
        {
            _companyJobSkill = new CompanyJobSkillPoco()
            {
                Id = Guid.NewGuid(),
                Importance = 2,
                Job = _companyJob.Id,
                Skill = Truncate(Faker.Lorem.Sentence(), 100),
                SkillLevel = String.Concat(Faker.Lorem.Letters(10))
            };
        }

        private void CompanyJobEducation_Init()
        {
            _companyJobEducation = new CompanyJobEducationPoco()
            {
                Id = Guid.NewGuid(),
                Importance = 2,
                Job = _companyJob.Id,
                Major = Truncate(Faker.Lorem.Sentence(), 100)
            };
        }

        private void CompanyJob_Init()
        {
            _companyJob = new CompanyJobPoco()
            {
                Id = Guid.NewGuid(),
                Company = _companyProfile.Id,
                IsCompanyHidden = false,
                IsInactive = false,
                ProfileCreated = Faker.Date.Past()
            };
        }

        private void CompanyDescription_Init()
        {
            _companyDescription = new CompanyDescriptionPoco()
            {
                Id = Guid.NewGuid(),
                CompanyDescription = Faker.Company.CatchPhrase(),
                CompanyName = Faker.Company.CatchPhrasePos(),
                LanguageId = _systemLangCode.LanguageID,
                Company = _companyProfile.Id
            };
        }

        private void SystemLangCode_Init()
        {
            _systemLangCode = new SystemLanguageCodePoco()
            {
                LanguageID = String.Concat(Faker.Lorem.Letters(10)),
                NativeName = Truncate(Faker.Lorem.Sentence(), 50),
                Name = Truncate(Faker.Lorem.Sentence(), 50)
            };
        }

        private void CompanyProfile_Init()
        {
            _companyProfile = new CompanyProfilePoco()
            {
                CompanyWebsite = "www.humber.ca",
                ContactName = Faker.Name.FullName(),
                ContactPhone = "416-555-8799",
                RegistrationDate = Faker.Date.Past(),
                Id = Guid.NewGuid(),
                CompanyLogo = new byte[10]
            };
        }

        private void SystemCountry_Init()
        {
            _systemCountry = new SystemCountryCodePoco
            {
                Code = String.Concat(Faker.Lorem.Letters(10)),
                Name = Truncate(Faker.Name.FullName(), 50)
            };
        }

        #endregion PocoInitialization

        [TestMethod]
        public void Assignment6_DeepDive_CRUD_Test()
        {
            SystemCountryCodeAdd();
            SystemLanguageCode_CRU_Test();

            CompanyProfileAdd();
            CompanyDescription_CRU_Test();
            CompanyJob_CRU_Test(); 
            CompanyJobEducation_CRU_Test();

            SecurityLogin_CRU_Test();
            SecurityLoginsLog_CRU_Test();

            ApplicantProfile_CRU_Test();
            ApplicantEducation_CRU_Test();
            ApplicantJobApplication_CRU_Test();

            // &&&&&&&&&&&&&&&&&&&&&&&&&
            #region cleanup
            ApplicantJobApplication_D_Test();
            ApplicantEducation_D_Test();
            ApplicantProfile_D_Test();

            SecurityLoginsLog_D_Test();
            SecurityLogin_D_Test();

            CompanyJobEducation_D_Test();
            CompanyJob_D_Test();
            CompanyDescription_D_Test();
            CompanyProfileRemove();

            SystemLanguageCode_D_Test();
            SystemCountryCodeRemove();
            #endregion cleanup
        }
        #region SystemLanguageCode_Block
        private void SystemLanguageCode_CRU_Test()
        { 
            var client = new SystemLanguageCode.SystemLanguageCodeClient(_channel);
            // add 
            SystemLanguageCodeProto proto = ProtoMapper.mapFromSystemLanguageCodePoco(_systemLangCode);
            SystemLanguageCodeList protos = new SystemLanguageCodeList();
            protos.Items.Add(proto);
            client.AddSystemLanguageCode(protos);

            CheckGetSystemLanguageCode(client, new SystemLanguageCodeKey() { LanguageID = proto.LanguageID }, proto);

            // check List
            protos = client.GetSystemLanguageCodes(new Empty());
            Assert.IsTrue(protos.Items.Count > 0);

            // check update
            proto.Name = Truncate(Faker.Lorem.Sentence(), 50);
            proto.NativeName = Truncate(Faker.Lorem.Sentence(), 50);

            protos = new SystemLanguageCodeList();
            protos.Items.Add(proto);
            client.UpdateSystemLanguageCode(protos);

            CheckGetSystemLanguageCode(client, new SystemLanguageCodeKey() { LanguageID = proto.LanguageID }, proto);

        }
        private void SystemLanguageCode_D_Test()
        {
            var client = new SystemLanguageCode.SystemLanguageCodeClient(_channel);

            SystemLanguageCodeProto proto = client.GetSystemLanguageCode(new SystemLanguageCodeKey() { LanguageID = _systemLangCode.LanguageID });
            SystemLanguageCodeList protos = new SystemLanguageCodeList();
            protos.Items.Add(proto);
            client.DeleteSystemLanguageCode(protos);
            proto = null;
            try
            {
                proto = client.GetSystemLanguageCode(new SystemLanguageCodeKey() { LanguageID = _systemLangCode.LanguageID });
            }
            catch (RpcException)
            {
                
            }
            Assert.IsNull(proto);
        }
        private SystemLanguageCodeProto CheckGetSystemLanguageCode(SystemLanguageCode.SystemLanguageCodeClient client
            , SystemLanguageCodeKey key, SystemLanguageCodeProto compare = null)
        {
            SystemLanguageCodeProto proto = null;
            try
            {
                proto = client.GetSystemLanguageCode(key);
            }
            catch (RpcException)
            {
                Assert.Fail();
            }
            Assert.IsNotNull(proto);
            Assert.AreEqual(proto.LanguageID, key.LanguageID);
            if(compare != null)
            {
                Assert.AreEqual(proto.Name, compare.Name);
                Assert.AreEqual(proto.NativeName, compare.NativeName);
            }
            return proto;
        }
        #endregion SystemLanguageCode_Block

        #region CompanyDescription_Block
        private void CompanyDescription_CRU_Test()
        {
            var client = new CompanyDescription.CompanyDescriptionClient(_channel);
            // add 
            CompanyDescriptionProto proto = ProtoMapper.mapFromCompanyDescriptionPoco(_companyDescription);
            CompanyDescriptionList protos = new CompanyDescriptionList();
            protos.Items.Add(proto);
            client.AddCompanyDescription(protos);

            proto = CheckGetCompanyDescription(client, new CompanyDescriptionKey() { Id = proto.Id }, proto);

            // check List
            protos = client.GetCompanyDescriptions(new Empty());
            Assert.IsTrue(protos.Items.Count > 0);

            // check update
            proto.CompanyDescription = Truncate(Faker.Lorem.Paragraph(), 500);
            proto.CompanyName = Truncate(Faker.Lorem.Sentence(), 50);

            protos = new CompanyDescriptionList();
            protos.Items.Add(proto);
            client.UpdateCompanyDescription(protos);

            CheckGetCompanyDescription(client, new CompanyDescriptionKey() { Id = proto.Id }, proto);

        }
        private void CompanyDescription_D_Test()
        {
            var client = new CompanyDescription.CompanyDescriptionClient(_channel);

            CompanyDescriptionProto proto = client.GetCompanyDescription(new CompanyDescriptionKey() { Id = _companyDescription.Id.ToString() });
            CompanyDescriptionList protos = new CompanyDescriptionList();
            protos.Items.Add(proto);
            client.DeleteCompanyDescription(protos);
            proto = null;
            try
            {
                proto = client.GetCompanyDescription(new CompanyDescriptionKey() { Id = _companyDescription.Id.ToString() });
            }
            catch (RpcException)
            {

            }
            Assert.IsNull(proto);
        }
        private CompanyDescriptionProto CheckGetCompanyDescription(CompanyDescription.CompanyDescriptionClient client
            , CompanyDescriptionKey key, CompanyDescriptionProto compare = null)
        {
            CompanyDescriptionProto proto = null;
            try
            {
                proto = client.GetCompanyDescription(key);
            }
            catch (RpcException)
            {
                Assert.Fail();
            }
            Assert.IsNotNull(proto);
            Assert.AreEqual(proto.Id, key.Id);
            if (compare != null)
            {
                Assert.AreEqual(proto.Company, compare.Company);
                Assert.AreEqual(proto.LanguageId, compare.LanguageId);
                Assert.AreEqual(proto.CompanyName, compare.CompanyName);
                Assert.AreEqual(proto.CompanyDescription, compare.CompanyDescription);

            } 
            return proto;
        }
        #endregion CompanyJob_Block

        #region CompanyJob_Block
        private void CompanyJob_CRU_Test()
        {
            var client = new CompanyJob.CompanyJobClient(_channel);
            // add 
            CompanyJobProto proto = ProtoMapper.mapFromCompanyJobPoco(_companyJob);
            CompanyJobList protos = new CompanyJobList();
            protos.Items.Add(proto);
            client.AddCompanyJob(protos);

            proto = CheckGetCompanyJob(client, new CompanyJobKey() { Id = proto.Id }, proto);

            // check List
            protos = client.GetCompanyJobs(new Empty());
            Assert.IsTrue(protos.Items.Count > 0);

            // check update
            proto.IsCompanyHidden = true;
            proto.IsInactive = true;
            proto.ProfileCreated = ConvertDateTime2TimeStamp(Faker.Date.Past());

            protos = new CompanyJobList();
            protos.Items.Add(proto);
            client.UpdateCompanyJob(protos);

            CheckGetCompanyJob(client, new CompanyJobKey() { Id = proto.Id }, proto);

        }
        private void CompanyJob_D_Test()
        {
            var client = new CompanyJob.CompanyJobClient(_channel);

            CompanyJobProto proto = client.GetCompanyJob(new CompanyJobKey() { Id = _companyJob.Id.ToString() });
            CompanyJobList protos = new CompanyJobList();
            protos.Items.Add(proto);
            client.DeleteCompanyJob(protos);
            proto = null;
            try
            {
                proto = client.GetCompanyJob(new CompanyJobKey() { Id = _companyJob.Id.ToString() });
            }
            catch (RpcException)
            {

            }
            Assert.IsNull(proto);
        }
        private CompanyJobProto CheckGetCompanyJob(CompanyJob.CompanyJobClient client
            , CompanyJobKey key, CompanyJobProto compare = null)
        {
            CompanyJobProto proto = null;
            try
            {
                proto = client.GetCompanyJob(key);
            }
            catch (RpcException)
            {
                Assert.Fail();
            }
            Assert.IsNotNull(proto);
            Assert.AreEqual(proto.Id, key.Id);
            if (compare != null)
            {
                Assert.AreEqual(proto.Company, compare.Company);
                Assert.AreEqual(proto.ProfileCreated, compare.ProfileCreated);
                Assert.AreEqual(proto.IsInactive, compare.IsInactive);
                Assert.AreEqual(proto.IsCompanyHidden, compare.IsCompanyHidden);

            }
            return proto;
        }
        #endregion CompanyJob_Block

        #region CompanyJobEducation_Block
        private void CompanyJobEducation_CRU_Test()
        {
            var client = new CompanyJobEducation.CompanyJobEducationClient(_channel);
            // add 
            CompanyJobEducationProto proto = ProtoMapper.mapFromCompanyJobEducationPoco(_companyJobEducation);
            CompanyJobEducationList protos = new CompanyJobEducationList();
            protos.Items.Add(proto);
            client.AddCompanyJobEducation(protos);

            proto = CheckGetCompanyJobEducation(client, new CompanyJobEducationKey() { Id = proto.Id }, proto);

            // check List
            protos = client.GetCompanyJobEducations(new Empty());
            Assert.IsTrue(protos.Items.Count > 0);

            // check update
            proto.Importance = 1;
            proto.Major = Truncate(Faker.Lorem.Sentence(), 100);

            protos = new CompanyJobEducationList();
            protos.Items.Add(proto);
            client.UpdateCompanyJobEducation(protos);

            CheckGetCompanyJobEducation(client, new CompanyJobEducationKey() { Id = proto.Id }, proto);

        }
        private void CompanyJobEducation_D_Test()
        {
            var client = new CompanyJobEducation.CompanyJobEducationClient(_channel);

            CompanyJobEducationProto proto = client.GetCompanyJobEducation(new CompanyJobEducationKey() { Id = _companyJobEducation.Id.ToString() });
            CompanyJobEducationList protos = new CompanyJobEducationList();
            protos.Items.Add(proto);
            client.DeleteCompanyJobEducation(protos);
            proto = null;
            try
            {
                proto = client.GetCompanyJobEducation(new CompanyJobEducationKey() { Id = _companyJobEducation.Id.ToString() });
            }
            catch (RpcException)
            {

            }
            Assert.IsNull(proto);
        }
        private CompanyJobEducationProto CheckGetCompanyJobEducation(CompanyJobEducation.CompanyJobEducationClient client
            , CompanyJobEducationKey key, CompanyJobEducationProto compare = null)
        {
            CompanyJobEducationProto proto = null;
            try
            {
                proto = client.GetCompanyJobEducation(key);
            }
            catch (RpcException)
            {
                Assert.Fail();
            }
            Assert.IsNotNull(proto);
            Assert.AreEqual(proto.Id, key.Id);
            if (compare != null)
            {
                Assert.AreEqual(proto.Job, compare.Job);
                Assert.AreEqual(proto.Major, compare.Major);
                Assert.AreEqual(proto.Importance, compare.Importance);
            }
            return proto;
        }
        #endregion CompanyJobEducation_Block

        #region SecurityLogin_Block
        private void SecurityLogin_CRU_Test()
        {
            var client = new SecurityLogin.SecurityLoginClient(_channel);
            // add 
            SecurityLoginProto proto = ProtoMapper.mapFromSecurityLoginPoco(_securityLogin);
            SecurityLoginList protos = new SecurityLoginList();
            protos.Items.Add(proto);
            client.AddSecurityLogin(protos);

            proto = CheckGetSecurityLogin(client, new SecurityLoginKey() { Id = proto.Id }, proto);

            // check List
            protos = client.GetSecurityLogins(new Empty());
            Assert.IsTrue(protos.Items.Count > 0);

            // check update
            proto.Login = Faker.User.Email();
            proto.AgreementAccepted = ConvertDateTime2TimeStamp(Faker.Date.PastWithTime());
            proto.Created = ConvertDateTime2TimeStamp(Faker.Date.PastWithTime());
            proto.EmailAddress = Faker.User.Email();
            proto.ForceChangePassword = true;
            proto.FullName = Faker.Name.FullName();
            proto.IsInactive = true;
            proto.IsLocked = true;
            proto.Password = "SoMePassWord@&@";
            proto.PasswordUpdate = ConvertDateTime2TimeStamp(Faker.Date.Forward());
            proto.PhoneNumber = "416-416-9889";
            proto.PrefferredLanguage = "FR".PadRight(10);

            protos = new SecurityLoginList();
            protos.Items.Add(proto);
            client.UpdateSecurityLogin(protos);

            CheckGetSecurityLogin(client, new SecurityLoginKey() { Id = proto.Id }, proto);

        }
        private void SecurityLogin_D_Test()
        {
            var client = new SecurityLogin.SecurityLoginClient(_channel);

            SecurityLoginProto proto = client.GetSecurityLogin(new SecurityLoginKey() { Id = _securityLogin.Id.ToString() });
            SecurityLoginList protos = new SecurityLoginList();
            protos.Items.Add(proto);
            client.DeleteSecurityLogin(protos);
            proto = null;
            try
            {
                proto = client.GetSecurityLogin(new SecurityLoginKey() { Id = _securityLogin.Id.ToString() });
            }
            catch (RpcException)
            {

            }
            Assert.IsNull(proto);
        }
        private SecurityLoginProto CheckGetSecurityLogin(SecurityLogin.SecurityLoginClient client
            , SecurityLoginKey key, SecurityLoginProto compare = null)
        {
            SecurityLoginProto proto = null;
            try
            {
                proto = client.GetSecurityLogin(key);
            }
            catch (RpcException)
            {
                Assert.Fail();
            }
            Assert.IsNotNull(proto);
            Assert.AreEqual(proto.Id, key.Id);
            if (compare != null)
            {
                Assert.AreEqual(proto.Login, compare.Login);
                //   Assert.AreEqual(proto.PasswordUpdate, compare.PasswordUpdate);
                Assert.AreEqual(proto.AgreementAccepted, compare.AgreementAccepted);
                //   Assert.AreEqual(proto.IsLocked, compare.IsLocked);
                //   Assert.AreEqual(proto.IsInactive, compare.IsInactive);
                Assert.AreEqual(proto.EmailAddress, compare.EmailAddress);
                Assert.AreEqual(proto.PhoneNumber, compare.PhoneNumber);
                Assert.AreEqual(proto.FullName, compare.FullName);
                //   Assert.AreEqual(proto.ForceChangePassword, compare.ForceChangePassword);
                Assert.AreEqual(proto.PrefferredLanguage, compare.PrefferredLanguage);

            }
            return proto;
        }
        #endregion SecurityLogin_Block

        #region SecurityLoginsLog_Block
        private void SecurityLoginsLog_CRU_Test()
        {
            var client = new SecurityLoginsLog.SecurityLoginsLogClient(_channel);
            // add 
            SecurityLoginsLogProto proto = ProtoMapper.mapFromSecurityLoginsLogPoco(_securityLoginLog);
            SecurityLoginsLogList protos = new SecurityLoginsLogList();
            protos.Items.Add(proto);
            client.AddSecurityLoginsLog(protos);

            proto = CheckGetSecurityLoginsLog(client, new SecurityLoginsLogKey() { Id = proto.Id }, proto);

            // check List
            protos = client.GetSecurityLoginsLogs(new Empty());
            Assert.IsTrue(protos.Items.Count > 0);

            // check update
            proto.IsSuccesful = false;
            proto.LogonDate = ConvertDateTime2TimeStamp(Faker.Date.PastWithTime());
            proto.SourceIP = Faker.Internet.IPv4().PadRight(15);

            protos = new SecurityLoginsLogList();
            protos.Items.Add(proto);
            client.UpdateSecurityLoginsLog(protos); // e9261fa9-f0c3-4603-b400-63a5f26952c7

            CheckGetSecurityLoginsLog(client, new SecurityLoginsLogKey() { Id = proto.Id }, proto);

        }
        private void SecurityLoginsLog_D_Test()
        {
            var client = new SecurityLoginsLog.SecurityLoginsLogClient(_channel);

            SecurityLoginsLogProto proto = client.GetSecurityLoginsLog(new SecurityLoginsLogKey() { Id = _securityLoginLog.Id.ToString() });
            SecurityLoginsLogList protos = new SecurityLoginsLogList();
            protos.Items.Add(proto);
            client.DeleteSecurityLoginsLog(protos);
            proto = null;
            try
            {
                proto = client.GetSecurityLoginsLog(new SecurityLoginsLogKey() { Id = _securityLoginLog.Id.ToString() });
            }
            catch (RpcException)
            {

            }
            Assert.IsNull(proto);
        }
        private SecurityLoginsLogProto CheckGetSecurityLoginsLog(SecurityLoginsLog.SecurityLoginsLogClient client
            , SecurityLoginsLogKey key, SecurityLoginsLogProto compare = null)
        {
            SecurityLoginsLogProto proto = null;
            try
            {
                proto = client.GetSecurityLoginsLog(key);
            }
            catch (RpcException)
            {
                Assert.Fail();
            }
            Assert.IsNotNull(proto);
            Assert.AreEqual(proto.Id, key.Id);
            if (compare != null)
            {
                Assert.AreEqual(proto.Login, compare.Login);
                Assert.AreEqual(proto.SourceIP, compare.SourceIP);
                Assert.AreEqual(proto.LogonDate.ToDateTime().Date, compare.LogonDate.ToDateTime().Date);

            }
            return proto;
        }
        #endregion SecurityLoginsLog_Block

        #region ApplicantProfile_Block
        private void ApplicantProfile_CRU_Test()
        {
            var client = new ApplicantProfile.ApplicantProfileClient(_channel);
            // add 
            ApplicantProfileProto proto = ProtoMapper.mapFromApplicantProfilePoco(_applicantProfile);
            ApplicantProfileList protos = new ApplicantProfileList();
            protos.Items.Add(proto);
            client.AddApplicantProfile(protos);

            proto = CheckGetApplicantProfile(client, new ApplicantProfileKey() { Id = proto.Id }, proto);

            // check List
            protos = client.GetApplicantProfiles(new Empty());
            Assert.IsTrue(protos.Items.Count > 0);

            // check update
            proto.City = Faker.Address.CityPrefix();
            proto.Currency = "US".PadRight(10);
            proto.CurrentRate = proto.CurrentRate.ConvertFrom(61.25M);
            proto.CurrentSalary = proto.CurrentSalary.ConvertFrom(77500);
            proto.Province = Truncate(Faker.Address.Province(), 10).PadRight(10);
            proto.Street = Truncate(Faker.Address.StreetName(), 100);
            proto.PostalCode = Truncate(Faker.Address.CanadianZipCode(), 20).PadRight(20);

            protos = new ApplicantProfileList();
            protos.Items.Add(proto);
            client.UpdateApplicantProfile(protos); // e9261fa9-f0c3-4603-b400-63a5f26952c7

            CheckGetApplicantProfile(client, new ApplicantProfileKey() { Id = proto.Id }, proto);

        }
        private void ApplicantProfile_D_Test()
        {
            var client = new ApplicantProfile.ApplicantProfileClient(_channel);

            ApplicantProfileProto proto = client.GetApplicantProfile(new ApplicantProfileKey() { Id = _applicantProfile.Id.ToString() });
            ApplicantProfileList protos = new ApplicantProfileList();
            protos.Items.Add(proto);
            client.DeleteApplicantProfile(protos);
            proto = null;
            try
            {
                proto = client.GetApplicantProfile(new ApplicantProfileKey() { Id = _applicantProfile.Id.ToString() });
            }
            catch (RpcException)
            {

            }
            Assert.IsNull(proto);
        }
        private ApplicantProfileProto CheckGetApplicantProfile(ApplicantProfile.ApplicantProfileClient client
            , ApplicantProfileKey key, ApplicantProfileProto compare = null)
        {
            ApplicantProfileProto proto = null;
            try
            {
                proto = client.GetApplicantProfile(key);
            }
            catch (RpcException)
            {
                Assert.Fail();
            }
            Assert.IsNotNull(proto);
            Assert.AreEqual(proto.Id, key.Id);
            if (compare != null)
            {
                Assert.AreEqual(proto.Login, compare.Login);
                Assert.AreEqual(proto.CurrentSalary, compare.CurrentSalary);
                Assert.AreEqual(proto.CurrentRate, compare.CurrentRate);
                Assert.AreEqual(proto.Currency, compare.Currency);
                Assert.AreEqual(proto.Country, compare.Country);
                Assert.AreEqual(proto.Province, compare.Province);
                Assert.AreEqual(proto.Street, compare.Street);
                Assert.AreEqual(proto.City, compare.City);
                Assert.AreEqual(proto.PostalCode, compare.PostalCode);

            }
            return proto;
        }
        #endregion ApplicantProfile_Block

        #region ApplicantEducation_Block
        private void ApplicantEducation_CRU_Test()
        {
            var client = new ApplicantEducation.ApplicantEducationClient(_channel);
            // add 
            ApplicantEducationProto proto = ProtoMapper.mapFromApplicantEducationPoco(_applicantEducation);
            ApplicantEducationList protos = new ApplicantEducationList();
            protos.Items.Add(proto);
            client.AddApplicantEducation(protos);

            proto = CheckGetApplicantEducation(client, new ApplicantEducationKey() { Id = proto.Id }, proto);

            // check List
            protos = client.GetApplicantEducations(new Empty());
            Assert.IsTrue(protos.Items.Count > 0);

            // check update
            proto.Major = Faker.Education.Major();
            proto.CertificateDiploma = Faker.Education.Major();
            proto.StartDate = ConvertDateTime2TimeStamp(Faker.Date.Past(3));
            proto.CompletionDate = ConvertDateTime2TimeStamp(Faker.Date.Forward(1));

            proto.CompletionPercent = (byte)Faker.Number.RandomNumber(1);

            protos = new ApplicantEducationList();
            protos.Items.Add(proto);
            client.UpdateApplicantEducation(protos); // e9261fa9-f0c3-4603-b400-63a5f26952c7

            CheckGetApplicantEducation(client, new ApplicantEducationKey() { Id = proto.Id }, proto);

        }
        private void ApplicantEducation_D_Test()
        {
            var client = new ApplicantEducation.ApplicantEducationClient(_channel);

            ApplicantEducationProto proto = client.GetApplicantEducation(new ApplicantEducationKey() { Id = _applicantEducation.Id.ToString() });
            ApplicantEducationList protos = new ApplicantEducationList();
            protos.Items.Add(proto);
            client.DeleteApplicantEducation(protos);
            proto = null;
            try
            {
                proto = client.GetApplicantEducation(new ApplicantEducationKey() { Id = _applicantEducation.Id.ToString() });
            }
            catch (RpcException)
            {

            }
            Assert.IsNull(proto);
        }
        private ApplicantEducationProto CheckGetApplicantEducation(ApplicantEducation.ApplicantEducationClient client
            , ApplicantEducationKey key, ApplicantEducationProto compare = null)
        {
            ApplicantEducationProto proto = null;
            try
            {
                proto = client.GetApplicantEducation(key);
            }
            catch (RpcException)
            {
                Assert.Fail();
            }
            Assert.IsNotNull(proto);
            Assert.AreEqual(proto.Id, key.Id);
            if (compare != null)
            {
                Assert.AreEqual(proto.Applicant, compare.Applicant);
                Assert.AreEqual(proto.Major, compare.Major);
                Assert.AreEqual(proto.CertificateDiploma, compare.CertificateDiploma);
                Assert.AreEqual(proto.StartDate.ToDateTime().Date, compare.StartDate.ToDateTime().Date);
                Assert.AreEqual(proto.CompletionDate.ToDateTime().Date, compare.CompletionDate.ToDateTime().Date);
                Assert.AreEqual(proto.CompletionPercent, compare.CompletionPercent);

            }
            return proto;
        }
        #endregion ApplicantEducation_Block

        #region ApplicantJobApplication_Block
        private void ApplicantJobApplication_CRU_Test()
        {
            var client = new ApplicantJobApplication.ApplicantJobApplicationClient(_channel);
            // add 
            ApplicantJobApplicationProto proto = ProtoMapper.mapFromApplicantJobApplicationPoco(_applicantJobApplication);
            ApplicantJobApplicationList protos = new ApplicantJobApplicationList();
            protos.Items.Add(proto);
            client.AddApplicantJobApplication(protos);

            proto = CheckGetApplicantJobApplication(client, new ApplicantJobApplicationKey() { Id = proto.Id }, proto);

            // check List
            protos = client.GetApplicantJobApplications(new Empty());
            Assert.IsTrue(protos.Items.Count > 0);

            // check update
            proto.ApplicationDate = ConvertDateTime2TimeStamp(Faker.Date.Recent());

            protos = new ApplicantJobApplicationList();
            protos.Items.Add(proto);
            client.UpdateApplicantJobApplication(protos); // e9261fa9-f0c3-4603-b400-63a5f26952c7

            CheckGetApplicantJobApplication(client, new ApplicantJobApplicationKey() { Id = proto.Id }, proto);

        }
        private void ApplicantJobApplication_D_Test()
        {
            var client = new ApplicantJobApplication.ApplicantJobApplicationClient(_channel);

            ApplicantJobApplicationProto proto = client.GetApplicantJobApplication(new ApplicantJobApplicationKey() { Id = _applicantJobApplication.Id.ToString() });
            ApplicantJobApplicationList protos = new ApplicantJobApplicationList();
            protos.Items.Add(proto);
            client.DeleteApplicantJobApplication(protos);
            proto = null;
            try
            {
                proto = client.GetApplicantJobApplication(new ApplicantJobApplicationKey() { Id = _applicantJobApplication.Id.ToString() });
            }
            catch (RpcException)
            {

            }
            Assert.IsNull(proto);
        }
        private ApplicantJobApplicationProto CheckGetApplicantJobApplication(ApplicantJobApplication.ApplicantJobApplicationClient client
            , ApplicantJobApplicationKey key, ApplicantJobApplicationProto compare = null)
        {
            ApplicantJobApplicationProto proto = null;
            try
            {
                proto = client.GetApplicantJobApplication(key);
            }
            catch (RpcException)
            {
                Assert.Fail();
            }
            Assert.IsNotNull(proto);
            Assert.AreEqual(proto.Id, key.Id);
            if (compare != null)
            {
                Assert.AreEqual(proto.Applicant, compare.Applicant);
                Assert.AreEqual(proto.Job, compare.Job);
                Assert.AreEqual(proto.ApplicationDate.ToDateTime().Date, compare.ApplicationDate.ToDateTime().Date);

            }
            return proto;
        }
        #endregion ApplicantJobApplication_Block

        #region Add_module_From_Assignment5
        private void SystemCountryCodeAdd()
        {
            SystemCountryCodeController controller = new SystemCountryCodeController();
            ActionResult result = controller.PostSystemCountryCode(new SystemCountryCodePoco[] { _systemCountry });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
        private void CompanyProfileAdd()
        {
            CompanyProfileController controller = new CompanyProfileController();
            ActionResult result = controller.PostCompanyProfile(new CompanyProfilePoco[] { _companyProfile });
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        #endregion Add_module_From_Assignment5
        #region Delete_module_From_Assignment5
        private void SystemCountryCodeRemove()
        {
            SystemCountryCodeController controller = new SystemCountryCodeController();
            ActionResult result = controller.DeleteSystemCountryCode(new SystemCountryCodePoco[] { _systemCountry });

        }
        private void CompanyProfileRemove()
        {
            CompanyProfileController controller = new CompanyProfileController();
            var result = controller.DeleteCompanyProfile(new CompanyProfilePoco[] { _companyProfile });
        }
        #endregion Delete_module_From_Assignment5

        private string Truncate(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return str.Length <= maxLength ? str : str.Substring(0, maxLength);
        }
        public static Timestamp ConvertDateTime2TimeStamp(DateTime input)
        {
            return Timestamp.FromDateTime(DateTime.SpecifyKind(input, DateTimeKind.Utc));
        }
    }
}
