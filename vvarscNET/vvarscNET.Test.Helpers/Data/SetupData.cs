using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Test.Helpers.Data
{
    public static class SetupData
    {
        #region Organizations
        public static List<Organization> _organizations = new List<Organization>()
        {
            new Organization
            {
                OrganizationName = "VVarMachine Navy",
                OrganizationSpectrumID = "VVAR",
                OrganizationWebsiteURL = "http://www.vvarmachine.com",
                IsActive = true
            }
        };

        #endregion

        #region PayGrades/Ranks
        public static List<PayGrade> _paygrades = new List<PayGrade>()
        {
            //Flag Officers
            new PayGrade
            {
                ID = 1,
                PayGradeName = "O-10",
                PayGradeDisplayName = "O-10",
                PayGradeOrderBy = 1,
                PayGradeGroup = "Flag Officer",
                IsActive = true
            },
            new PayGrade
            {
                ID = 2,
                PayGradeName = "O-9",
                PayGradeDisplayName = "O-9",
                PayGradeOrderBy = 2,
                PayGradeGroup = "Flag Officer",
                IsActive = true
            },
            new PayGrade
            {
                ID = 3,
                PayGradeName = "O-8",
                PayGradeDisplayName = "O-8",
                PayGradeOrderBy = 3,
                PayGradeGroup = "Flag Officer",
                IsActive = true
            },
            new PayGrade
            {
                ID = 4,
                PayGradeName = "O-7",
                PayGradeDisplayName = "O-7",
                PayGradeOrderBy = 4,
                PayGradeGroup = "Flag Officer",
                IsActive = true
            },
            //Senior Officers
            new PayGrade
            {
                ID = 5,
                PayGradeName = "O-6",
                PayGradeDisplayName = "O-6",
                PayGradeOrderBy = 5,
                PayGradeGroup = "Senior Officer",
                IsActive = true
            },
            new PayGrade
            {
                ID = 6,
                PayGradeName = "O-5",
                PayGradeDisplayName = "O-5",
                PayGradeOrderBy = 6,
                PayGradeGroup = "Senior Officer",
                IsActive = true
            },
            new PayGrade
            {
                ID = 7,
                PayGradeName = "O-4",
                PayGradeDisplayName = "O-4",
                PayGradeOrderBy = 7,
                PayGradeGroup = "Senior Officer",
                IsActive = true
            },
            //Officers
            new PayGrade
            {
                ID = 8,
                PayGradeName = "O-3",
                PayGradeDisplayName = "O-3",
                PayGradeOrderBy = 8,
                PayGradeGroup = "Officer",
                IsActive = true
            },
            new PayGrade
            {
                ID = 9,
                PayGradeName = "O-2",
                PayGradeDisplayName = "O-2",
                PayGradeOrderBy = 9,
                PayGradeGroup = "Officer",
                IsActive = true
            },
            new PayGrade
            {
                ID = 10,
                PayGradeName = "O-1",
                PayGradeDisplayName = "O-1",
                PayGradeOrderBy = 10,
                PayGradeGroup = "Officer",
                IsActive = true
            },
            //Officer Candidate
            new PayGrade
            {
                ID = 11,
                PayGradeName = "OC",
                PayGradeDisplayName = "OC",
                PayGradeOrderBy = 11,
                PayGradeGroup = "Officer Candidate",
                IsActive = true
            },
            //Senior NCOs
            new PayGrade
            {
                ID = 12,
                PayGradeName = "E-9 (Special)",
                PayGradeDisplayName = "E-9",
                PayGradeOrderBy = 12,
                PayGradeGroup = "Senior NCO",
                IsActive = true
            },
            new PayGrade
            {
                ID = 13,
                PayGradeName = "E-9",
                PayGradeDisplayName = "E-9",
                PayGradeOrderBy = 13,
                PayGradeGroup = "Senior NCO",
                IsActive = true
            },
            new PayGrade
            {
                ID = 14,
                PayGradeName = "E-8",
                PayGradeDisplayName = "E-8",
                PayGradeOrderBy = 14,
                PayGradeGroup = "Senior NCO",
                IsActive = true
            },
            new PayGrade
            {
                ID = 15,
                PayGradeName = "E-7",
                PayGradeDisplayName = "E-7",
                PayGradeOrderBy = 15,
                PayGradeGroup = "Senior NCO",
                IsActive = true
            },
            //NCOs
            new PayGrade
            {
                ID = 16,
                PayGradeName = "E-6",
                PayGradeDisplayName = "E-6",
                PayGradeOrderBy = 16,
                PayGradeGroup = "NCO",
                IsActive = true
            },
            new PayGrade
            {
                ID = 17,
                PayGradeName = "E-5",
                PayGradeDisplayName = "E-5",
                PayGradeOrderBy = 17,
                PayGradeGroup = "NCO",
                IsActive = true
            },
            new PayGrade
            {
                ID = 18,
                PayGradeName = "E-4",
                PayGradeDisplayName = "E-4",
                PayGradeOrderBy = 18,
                PayGradeGroup = "NCO",
                IsActive = true
            },
            //Enlisted
            new PayGrade
            {
                ID = 19,
                PayGradeName = "E-3",
                PayGradeDisplayName = "E-3",
                PayGradeOrderBy = 19,
                PayGradeGroup = "Member",
                IsActive = true
            },
            new PayGrade
            {
                ID = 20,
                PayGradeName = "E-2",
                PayGradeDisplayName = "E-2",
                PayGradeOrderBy = 20,
                PayGradeGroup = "Member",
                IsActive = true
            }
        };

        public static List<Rank> _ranks = new List<Rank>()
        {
            //Flag Officers
            new Rank
            {
                PayGradeID = 1,
                RankName = "Admiral",
                RankAbbr = "ADM",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O10.png",
                RankGroupName = "Flag Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 2,
                RankName = "Vice Admiral",
                RankAbbr = "VADM",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O9.png",
                RankGroupName = "Flag Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 3,
                RankName = "Rear Admiral",
                RankAbbr = "RADM",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O8.png",
                RankGroupName = "Flag Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 4,
                RankName = "Rear Admiral (Lower Half)",
                RankAbbr = "RDML",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                RankGroupName = "Flag Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 4,
                RankName = "Brigadier General",
                RankAbbr = "BGEN",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                RankGroupName = "Flag Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O7.png",
                IsActive = true
            },
            //Senior Officers
            new Rank
            {
                PayGradeID = 5,
                RankName = "Captain",
                RankAbbr = "CAPT",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 5,
                RankName = "Colonel",
                RankAbbr = "COL",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 6,
                RankName = "Commander",
                RankAbbr = "CDR",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O5.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 6,
                RankName = "Lieutenant Colonel",
                RankAbbr = "LTCOL",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O5.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 7,
                RankName = "Lieutenant Commander",
                RankAbbr = "LCDR",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O4.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 7,
                RankName = "Major",
                RankAbbr = "MAJ",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O4.png",
                RankGroupName = "Senior Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O6.png",
                IsActive = true
            },
            //Officers
            new Rank
            {
                PayGradeID = 8,
                RankName = "Lieutenant",
                RankAbbr = "LT",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 8,
                RankName = "Captain",
                RankAbbr = "CAPT",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 9,
                RankName = "Lieutenant (Junior Grade)",
                RankAbbr = "LT",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O2.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 9,
                RankName = "First Lieutenant",
                RankAbbr = "1LT",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O2.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 10,
                RankName = "Ensign",
                RankAbbr = "ENS",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O1.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 10,
                RankName = "Second Lieutenant",
                RankAbbr = "2LT",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Common/O1.png",
                RankGroupName = "Officer",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Common/O3.png",
                IsActive = true
            },
            //Officer Candidate
            new Rank
            {
                PayGradeID = 11,
                RankName = "Midshipman",
                RankAbbr = "MIDN",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/MIDN_4C.png",
                RankGroupName = "Officer Candidate",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/MIDN_4C.png",
                IsActive = true
            },
            //Senior NCO
            new Rank
            {
                PayGradeID = 12,
                RankName = "Fleet Master Chief Petty Officer",
                RankAbbr = "FMCPO",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E9_MCPON.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 13,
                RankName = "Master Chief Petty Officer",
                RankAbbr = "MCPO",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E9_GC.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 13,
                RankName = "Sergeant Major",
                RankAbbr = "SGM",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E9-SGM.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 14,
                RankName = "Senior Chief Petty Officer",
                RankAbbr = "SCPO",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E8_GC.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 14,
                RankName = "First Sergeant",
                RankAbbr = "1SG",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E8-1SG.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 15,
                RankName = "Chief Petty Officer",
                RankAbbr = "CPO",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 15,
                RankName = "Gunnery Sergeant",
                RankAbbr = "GYSGT",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E7.png",
                RankGroupName = "Senior NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E7_GC.png",
                IsActive = true
            },
            //NCOs
            new Rank
            {
                PayGradeID = 16,
                RankName = "Petty Officer 1st Class",
                RankAbbr = "PO1",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E6.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 16,
                RankName = "Staff Sergeant",
                RankAbbr = "SSGT",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E6.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 17,
                RankName = "Petty Officer 2nd Class",
                RankAbbr = "PO2",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E5.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 17,
                RankName = "Sergeant",
                RankAbbr = "SGT",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E5.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 18,
                RankName = "Petty Officer 3rd Class",
                RankAbbr = "PO3",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 18,
                RankName = "Corporal",
                RankAbbr = "CPL",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E4.png",
                RankGroupName = "NCO",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E4.png",
                IsActive = true
            },
            //Enlisted
            new Rank
            {
                PayGradeID = 19,
                RankName = "Airman",
                RankAbbr = "AN",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E3.png",
                RankGroupName = "Member",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E2.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 19,
                RankName = "Lance Corporal",
                RankAbbr = "LCPL",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E3.png",
                RankGroupName = "Member",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E2.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 20,
                RankName = "Airman Apprentice",
                RankAbbr = "AA",
                RankType = "Navy",
                RankImage = "https://sc.vvarmachine.com/images/ranks/Navy/E2.png",
                RankGroupName = "Member",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E2.png",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 20,
                RankName = "Private 1st Class",
                RankAbbr = "PFC",
                RankType = "Marine",
                RankImage = "https://sc.vvarmachine.com/images/ranks/USMC/E2.png",
                RankGroupName = "Member",
                RankGroupImage = "https://sc.vvarmachine.com/images/ranks/Navy/E2.png",
                IsActive = true
            }
        };
        #endregion

    }
}
