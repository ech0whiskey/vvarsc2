using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using vvarscNET.Core;
using vvarscNET.Core.Factories;
using vvarscNET.Core.Interfaces;
using vvarscNET.Core.QueryModels.Authentication;
using vvarscNET.Core.Data.QueryHandlers.Authentication;
using vvarscNET.Core.CommandModels.Authentication;
using vvarscNET.Core.Data.CommandHandlers.Authentication;
using vvarscNET.Core.QueryModels.Organizations;
using vvarscNET.Core.Data.QueryHandlers.Organizations;
using vvarscNET.Core.CommandModels.Organizations;
using vvarscNET.Core.Data.CommandHandlers.Organizations;
using vvarscNET.Core.QueryModels.People;
using vvarscNET.Core.Data.QueryHandlers.People;
using vvarscNET.Core.CommandModels.People;
using vvarscNET.Core.Data.CommandHandlers.People;

namespace vvarscNET.Test.Helpers.Data
{
    public class InitializeCoreTestDb
    {
        public static void Initialize(string connectionString, bool isTest = false)
        {
            if (!InitOrganization(connectionString))
                throw new Exception("Could not init organization");
            if (!InitOrgAdmins(connectionString))
                throw new Exception("Could not init organization admins");
            if (!InitSuperAdmin(connectionString))
                throw new Exception("Could not init superadmin");
            if (!InitPayGradesAndRanks(connectionString))
                throw new Exception("Could not init PayGrades and Ranks");
        }

        #region SuperAdmin
        private static bool InitSuperAdmin(string connectionString)
        {
            //Create Member
            string userName = "superadmin";
            var memCmd = new CreateMember_C
            {
                UserName = userName,
                RSIHandle = "invalid_handle",
                UserType = Model.Enums.UserTypeEnum.SuperAdmin,
                OrganizationID = null,
                IsActive = true
            };

            CreateMember_CH createMember_CH = new CreateMember_CH(new SQLConnectionFactory(connectionString));
            var memResult = createMember_CH.Handle(Globals.UserContext, memCmd);

            if (memResult.Status != System.Net.HttpStatusCode.OK)
            {
                return false;
            }
            var memID = Convert.ToInt32(memResult.ItemIDs.FirstOrDefault());

            //Create Credential for Member
            SHA256Managed hashalgo = new SHA256Managed();
            byte[] hash = hashalgo.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userName.ToLower()));
            string passwordHash = BitConverter.ToString(hash);
            passwordHash = passwordHash.Replace("-", "");

            var credCmd = new CreateCredential_C
            {
                MemberID = memID,
                UserName = userName,
                PasswordHash = passwordHash,
                OrganizationID = null
            };

            CreateCredential_CH createCredential_CH = new CreateCredential_CH(new SQLConnectionFactory(connectionString));
            var credResult = createCredential_CH.Handle(Globals.UserContext, credCmd);

            if (credResult.Status != System.Net.HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Organizations
        private static bool InitOrganization(string connectionString)
        {
            foreach (var t in SetupData._organizations)
            {
                var qry = new GetOrganizationBySpectrumID_Q
                {
                    SpectrumID = t.OrganizationSpectrumID
                };

                GetOrganizationBySpectrumID_QH getOrganizationBySpectrumID_QH = new GetOrganizationBySpectrumID_QH(new Core.Factories.SQLConnectionFactory(connectionString));
                var organization = getOrganizationBySpectrumID_QH.Handle(Globals.AuthHandlerToken, qry);

                if (organization == null)
                {
                    organization = Helpers.Clone(t);

                    var cmd = new CreateOrganization_C
                    {
                        OrganizationName = organization.OrganizationName,
                        OrganizationSpectrumID = organization.OrganizationSpectrumID,
                        OrganizationWebsiteURL = organization.OrganizationWebsiteURL,
                        IsActive = organization.IsActive
                    };

                    CreateOrganization_CH createOrganziationCommandHandler = new CreateOrganization_CH(new Core.Factories.SQLConnectionFactory(connectionString));
                    var cmdResult = createOrganziationCommandHandler.Handle(Globals.UserContext, cmd);

                    if (cmdResult.Status != System.Net.HttpStatusCode.OK)
                        return false;
                }
            }
            return true;
        }

        private static bool InitOrgAdmins(string connectionString)
        {
            foreach (var org in SetupData._organizations)
            {
                //Setup AdminUser for Each Organization
                var qry = new GetOrganizationBySpectrumID_Q
                {
                    SpectrumID = org.OrganizationSpectrumID
                };

                GetOrganizationBySpectrumID_QH getOrganizationBySpectrumID_QH = new GetOrganizationBySpectrumID_QH(new SQLConnectionFactory(connectionString));
                var organization = getOrganizationBySpectrumID_QH.Handle(Globals.AuthHandlerToken, qry);

                if (organization != null)
                {

                    //Create Member
                    string userName = organization.OrganizationSpectrumID.ToLower() + ".admin";
                    var memCmd = new CreateMember_C
                    {
                        UserName = userName,
                        RSIHandle = "invalid_handle",
                        UserType = Model.Enums.UserTypeEnum.Admin,
                        OrganizationID = organization.ID,
                        IsActive = true
                    };

                    CreateMember_CH createMember_CH = new CreateMember_CH(new SQLConnectionFactory(connectionString));
                    var memResult = createMember_CH.Handle(Globals.UserContext, memCmd);

                    if (memResult.Status != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }
                    var memID = Convert.ToInt32(memResult.ItemIDs.FirstOrDefault());

                    //Create Credential for Member
                    SHA256Managed hashalgo = new SHA256Managed();
                    byte[] hash = hashalgo.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userName.ToLower()));
                    string passwordHash = BitConverter.ToString(hash);
                    passwordHash = passwordHash.Replace("-", "");

                    var credCmd = new CreateCredential_C
                    {
                        MemberID = memID,
                        UserName = userName,
                        PasswordHash = passwordHash,
                        OrganizationID = organization.ID
                    };

                    CreateCredential_CH createCredential_CH = new CreateCredential_CH(new SQLConnectionFactory(connectionString));
                    var credResult = createCredential_CH.Handle(Globals.UserContext, credCmd);

                    if (credResult.Status != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region PayGrades/Ranks
        private static bool InitPayGradesAndRanks(string connectionString)
        {
            foreach (var pg in SetupData._paygrades)
            {
                //Create PayGrade
                var pgCmd = new CreatePayGrade_C
                {
                    PayGradeName = pg.PayGradeName,
                    PayGradeDisplayName = pg.PayGradeDisplayName,
                    PayGradeOrderBy = pg.PayGradeOrderBy,
                    PayGradeGroup = pg.PayGradeGroup,
                    PayGradeDescriptionText = pg.PayGradeDescriptionText,
                    PayGradeNotes = pg.PayGradeNotes,
                    IsActive = true
                };

                CreatePayGrade_CH createPayGrade_CH = new CreatePayGrade_CH(new SQLConnectionFactory(connectionString));
                var pgResult = createPayGrade_CH.Handle(Globals.UserContext, pgCmd);

                if (pgResult.Status != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }
                var pgID = Convert.ToInt32(pgResult.ItemIDs.FirstOrDefault());

                var pgRanks = SetupData._ranks.Where(a => a.PayGradeID == pgID).ToList();
                foreach (var rank in pgRanks)
                {
                    //Create Rank(s) for this PayGrade
                    var rankCmd = new CreateRank_C
                    {
                        PayGradeID = pgID,
                        RankName = rank.RankName,
                        RankAbbr = rank.RankAbbr,
                        RankType = rank.RankType,
                        RankImage = rank.RankImage,
                        RankGroupName = rank.RankGroupName,
                        RankGroupImage = rank.RankGroupImage,
                        IsActive = true
                    };

                    CreateRank_CH createRank_CH = new CreateRank_CH(new SQLConnectionFactory(connectionString));
                    var rankResult = createRank_CH.Handle(Globals.UserContext, rankCmd);

                    if (rankResult.Status != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion
    }
}
