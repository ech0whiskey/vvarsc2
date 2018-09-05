using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Test.Helpers.Data
{
    public static partial class SetupData
    {
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
                RankAbbr = "Col",
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
                RankAbbr = "LtCol",
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
                RankAbbr = "Maj",
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
                RankAbbr = "Capt",
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
                RankAbbr = "LTJG",
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
                RankAbbr = "1stLt",
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
                RankAbbr = "2ndLt",
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
                RatingCodeSuffix = "CM",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 13,
                RankName = "Sergeant Major",
                RankAbbr = "SgtMaj",
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
                RatingCodeSuffix = "CS",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 14,
                RankName = "First Sergeant",
                RankAbbr = "1stSgt",
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
                RatingCodeSuffix = "C",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 15,
                RankName = "Gunnery Sergeant",
                RankAbbr = "GySgt",
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
                RatingCodeSuffix = "1",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 16,
                RankName = "Staff Sergeant",
                RankAbbr = "SSgt",
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
                RatingCodeSuffix = "2",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 17,
                RankName = "Sergeant",
                RankAbbr = "Sgt",
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
                RatingCodeSuffix = "3",
                IsActive = true
            },
            new Rank
            {
                PayGradeID = 18,
                RankName = "Corporal",
                RankAbbr = "Cpl",
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
                RankAbbr = "LCpl",
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

    }
}
