using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.Organizations;
using vvarscNET.Model.Objects.People;

namespace vvarscNET.Test.Helpers.Data
{
    public static partial class SetupData
    {
        public static List<OrgRole> _orgRoles = new List<OrgRole>()
        {
            //FleetCom
            new OrgRole {
                RoleOrderBy = 100,
                RoleName = "Fleet Commanding Officer",
                RoleShortName = "Fleet CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "FLEETCOM",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-10" } }
            },
            new OrgRole {
                RoleOrderBy = 101,
                RoleName = "Fleet Executive Officer",
                RoleShortName = "Fleet XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "FLEETCOM",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-9" } }
            },
            new OrgRole {
                RoleOrderBy = 102,
                RoleName = "Chief of Naval Operations",
                RoleShortName = "CNO",
                RoleDisplayName = "Chief of Naval Operations",
                RoleType = "FLEETCOM",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-8" } }
            },
            new OrgRole {
                RoleOrderBy = 105,
                RoleName = "Fleet Senior Enlisted Adviser",
                RoleShortName = "Fleet Adviser",
                RoleDisplayName = "Senior Enlisted Adviser",
                RoleType = "FLEETCOM",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-9 (Special)" } }
            },
            new OrgRole {
                RoleOrderBy = 106,
                RoleName = "Fleet Enlisted Adviser",
                RoleShortName = "Fleet Adviser",
                RoleDisplayName = "Enlisted Adviser",
                RoleType = "FLEETCOM",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-9" } }
            },

            //NAVCOM HQ
            new OrgRole {
                RoleOrderBy = 110,
                RoleName = "Naval Command Commanding Officer",
                RoleShortName = "NAVCOM CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "NAVCOM HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-7" }, new PayGrade { PayGradeName = "O-6"} }
            },
            new OrgRole {
                RoleOrderBy = 111,
                RoleName = "Naval Command Executive Officer",
                RoleShortName = "NAVCOM XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "NAVCOM HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-5" }, new PayGrade { PayGradeName = "O-6"} }
            },
            new OrgRole {
                RoleOrderBy = 112,
                RoleName = "Naval Command Senior Enlisted Adviser",
                RoleShortName = "NAVCOM Adviser",
                RoleDisplayName = "Senior Enlisted Adviser",
                RoleType = "NAVCOM HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-9" } }
            },

            //Office Leadership
            new OrgRole {
                RoleOrderBy = 115,
                RoleName = "Senior Technology Officer",
                RoleShortName = "Senior Tech Officer",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Office Leadership",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-4" }, new PayGrade { PayGradeName = "O-5"} }
            },
            new OrgRole {
                RoleOrderBy = 116,
                RoleName = "Senior Intelligence Officer",
                RoleShortName = "Senior Intel Officer",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Office Leadership",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-4" }, new PayGrade { PayGradeName = "O-5"} }
            },
            new OrgRole {
                RoleOrderBy = 117,
                RoleName = "Senior Finance Officer",
                RoleShortName = "Senior Financier",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Office Leadership",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-4" }, new PayGrade { PayGradeName = "O-5"} }
            },
            new OrgRole {
                RoleOrderBy = 118,
                RoleName = "Senior Public Relations Officer",
                RoleShortName = "Senior PR Officer",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Office Leadership",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-4" }, new PayGrade { PayGradeName = "O-5"} }
            },
            new OrgRole {
                RoleOrderBy = 119,
                RoleName = "Senior Diplomacy Officer",
                RoleShortName = "Senior Diplomat",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Office Leadership",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-4" }, new PayGrade { PayGradeName = "O-5"} }
            },

            //AIR WING HQ
            new OrgRole {
                RoleOrderBy = 120,
                RoleName = "Air Wing Commanding Officer",
                RoleShortName = "Air Wing CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Air Wing HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-4" }, new PayGrade { PayGradeName = "O-5"} }
            },
            new OrgRole {
                RoleOrderBy = 121,
                RoleName = "Air Wing Executive Officer",
                RoleShortName = "Air Wing XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "Air Wing HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-3" }, new PayGrade { PayGradeName = "O-4"} }
            },
            new OrgRole {
                RoleOrderBy = 122,
                RoleName = "Air Wing Chief",
                RoleShortName = "Air Wing Chief",
                RoleType = "Air Wing HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-8" } }
            },

            //Combat Team HQ
            new OrgRole {
                RoleOrderBy = 130,
                RoleName = "Combat Team Commanding Officer",
                RoleShortName = "Combat Team CO",
                RoleDisplayName = "Commanding Officer",
                RoleType = "Combat Team HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-4" }, new PayGrade { PayGradeName = "O-5"} }
            },
            new OrgRole {
                RoleOrderBy = 131,
                RoleName = "Combat Team Executive Officer",
                RoleShortName = "Combat Team XO",
                RoleDisplayName = "Executive Officer",
                RoleType = "Combat Team HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-3" }, new PayGrade { PayGradeName = "O-4"} }
            },
            new OrgRole {
                RoleOrderBy = 132,
                RoleName = "Combat Team Sergeant",
                RoleShortName = "Team Sergeant",
                RoleType = "Combat Team HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-8" } }
            },

            //Squadron & Platoon Leadership
            new OrgRole {
                RoleOrderBy = 140,
                RoleName = "Squadron Leader",
                RoleType = "Squadron HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-1" }, new PayGrade { PayGradeName = "O-2" }, new PayGrade { PayGradeName = "O-3"} }
            },
            new OrgRole {
                RoleOrderBy = 141,
                RoleName = "Platoon Leader",
                RoleType = "Platoon HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "O-1" }, new PayGrade { PayGradeName = "O-2" }, new PayGrade { PayGradeName = "O-3"} }
            },
            new OrgRole {
                RoleOrderBy = 141,
                RoleName = "Officer-in-Training",
                RoleShortName = "OIT",
                RoleType = "OIT",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "OC" } }
            },
            new OrgRole {
                RoleOrderBy = 145,
                RoleName = "Squadron Chief",
                RoleShortName = "Squadron Chief",
                RoleType = "Squadron HQ",
                RatingCode = "AP",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-6" }, new PayGrade { PayGradeName = "E-7" } }
            },
            new OrgRole {
                RoleOrderBy = 146,
                RoleName = "Platoon Sergeant",
                RoleShortName = "Platoon Sgt.",
                RoleType = "Platoon HQ",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-6" }, new PayGrade { PayGradeName = "E-7" } }
            },
            new OrgRole {
                RoleOrderBy = 147,
                RoleName = "Flight Leader",
                RoleType = "Tactical Flight Leadership",
                RatingCode = "AP",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-5" }, new PayGrade { PayGradeName = "E-6" } }
            },
            new OrgRole {
                RoleOrderBy = 148,
                RoleName = "Crew Chief",
                RoleType = "Tactical Flight Leadership",
                RatingCode = "AW",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-5" }, new PayGrade { PayGradeName = "E-6" } }
            },
            new OrgRole {
                RoleOrderBy = 148,
                RoleName = "Squad Leader",
                RoleType = "Tactical Marine Leadership",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-5" }, new PayGrade { PayGradeName = "E-6" } }
            },

            //Squadron Specialist & Support
            new OrgRole {
                RoleOrderBy = 149,
                RoleName = "Squadron Intelligence Specialist",
                RoleShortName = "Intel Specialist",
                RoleType = "Flight",
                RatingCode = "IS",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-5" }, new PayGrade { PayGradeName = "E-6" }, new PayGrade { PayGradeName = "E-7" } }
            },

            //Aviation Roles
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Airlift Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Attack Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Combat Search & Rescue Operator",
                RoleShortName = "CSAR Operator",
                RoleType = "Flight",
                RatingCode = "AW",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Combat Search & Rescue Pilot",
                RoleShortName = "CSAR Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Combat Transport Pilot",
                RoleShortName = "ComTrans Operator",
                RoleType = "Flight",
                RatingCode = "AP",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Electronic Warfare Pilot",
                RoleShortName = "EWAR Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Electronic Warfare Technician",
                RoleShortName = "EWAR Tech",
                RoleType = "Flight",
                RatingCode = "ET",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Exploration Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Fighter Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Flight Engineer",
                RoleType = "Flight",
                RatingCode = "AD",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Loadmaster",
                RoleType = "Flight",
                RatingCode = "AW",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Operations Specialist",
                RoleShortName = "Ops Specialist",
                RoleType = "Flight",
                RatingCode = "AW",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Reconaissance Operator",
                RoleShortName = "Recon Operator",
                RoleType = "Flight",
                RatingCode = "SO",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-5" }, new PayGrade { PayGradeName = "E-6" } }
            },
            new OrgRole {
                RoleOrderBy = 150,
                RoleName = "Strike Pilot",
                RoleType = "Flight",
                RatingCode = "AP",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 159,
                RoleName = "Flight Trainee",
                RoleType = "Flight",
                RatingCode = "AP",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" } }
            },
            
            //Marine Roles
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Corpsman",
                RoleType = "Marine",
                RatingCode = "HM",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Demolitions Specialist",
                RoleShortName = "Demo Specialist",
                RoleType = "Marine",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Heavy Weapons Operator",
                RoleShortName = "Weapons Operator",
                RoleType = "Marine",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Marksman",
                RoleType = "Marine",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Rifleman",
                RoleType = "Marine",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" }, new PayGrade { PayGradeName = "E-4" }, new PayGrade { PayGradeName = "E-5" } }
            },
            new OrgRole {
                RoleOrderBy = 160,
                RoleName = "Infantry Trainee",
                RoleType = "Marine",
                SupportedPayGrades = new List<PayGrade> { new PayGrade { PayGradeName = "E-3" } }
            },

            //Office Roles - Officers
            new OrgRole {
                RoleOrderBy = 170,
                RoleName = "Technology Officer",
                RoleShortName = "Tech Officer",
                RoleType = "Office",
                SupportedPayGrades = new List<PayGrade> {
                    new PayGrade { PayGradeName = "O-1" },
                    new PayGrade { PayGradeName = "O-2" },
                    new PayGrade { PayGradeName = "O-3" },
                    new PayGrade { PayGradeName = "O-4" },
                    new PayGrade { PayGradeName = "O-5" },
                    new PayGrade { PayGradeName = "O-6" },
                    new PayGrade { PayGradeName = "O-7" },
                    new PayGrade { PayGradeName = "O-8" },
                    new PayGrade { PayGradeName = "O-9" },
                    new PayGrade { PayGradeName = "O-10" },
                }
            },
            new OrgRole {
                RoleOrderBy = 171,
                RoleName = "Intelligence Officer",
                RoleShortName = "Intel Officer",
                RoleType = "Office",
                SupportedPayGrades = new List<PayGrade> {
                    new PayGrade { PayGradeName = "O-1" },
                    new PayGrade { PayGradeName = "O-2" },
                    new PayGrade { PayGradeName = "O-3" },
                    new PayGrade { PayGradeName = "O-4" },
                    new PayGrade { PayGradeName = "O-5" },
                    new PayGrade { PayGradeName = "O-6" },
                    new PayGrade { PayGradeName = "O-7" },
                    new PayGrade { PayGradeName = "O-8" },
                    new PayGrade { PayGradeName = "O-9" },
                    new PayGrade { PayGradeName = "O-10" },
                }
            },
            new OrgRole {
                RoleOrderBy = 172,
                RoleName = "Finance Officer",
                RoleShortName = "Financier",
                RoleType = "Office",
                SupportedPayGrades = new List<PayGrade> {
                    new PayGrade { PayGradeName = "O-1" },
                    new PayGrade { PayGradeName = "O-2" },
                    new PayGrade { PayGradeName = "O-3" },
                    new PayGrade { PayGradeName = "O-4" },
                    new PayGrade { PayGradeName = "O-5" },
                    new PayGrade { PayGradeName = "O-6" },
                    new PayGrade { PayGradeName = "O-7" },
                    new PayGrade { PayGradeName = "O-8" },
                    new PayGrade { PayGradeName = "O-9" },
                    new PayGrade { PayGradeName = "O-10" },
                }
            },
            new OrgRole {
                RoleOrderBy = 173,
                RoleName = "Public Relations Officer",
                RoleShortName = "PR Officer",
                RoleType = "Office",
                SupportedPayGrades = new List<PayGrade> {
                    new PayGrade { PayGradeName = "O-1" },
                    new PayGrade { PayGradeName = "O-2" },
                    new PayGrade { PayGradeName = "O-3" },
                    new PayGrade { PayGradeName = "O-4" },
                    new PayGrade { PayGradeName = "O-5" },
                    new PayGrade { PayGradeName = "O-6" },
                    new PayGrade { PayGradeName = "O-7" },
                    new PayGrade { PayGradeName = "O-8" },
                    new PayGrade { PayGradeName = "O-9" },
                    new PayGrade { PayGradeName = "O-10" },
                }
            },
            new OrgRole {
                RoleOrderBy = 174,
                RoleName = "Diplomacy Officer",
                RoleShortName = "Diplomat",
                RoleType = "Office",
                SupportedPayGrades = new List<PayGrade> {
                    new PayGrade { PayGradeName = "O-1" },
                    new PayGrade { PayGradeName = "O-2" },
                    new PayGrade { PayGradeName = "O-3" },
                    new PayGrade { PayGradeName = "O-4" },
                    new PayGrade { PayGradeName = "O-5" },
                    new PayGrade { PayGradeName = "O-6" },
                    new PayGrade { PayGradeName = "O-7" },
                    new PayGrade { PayGradeName = "O-8" },
                    new PayGrade { PayGradeName = "O-9" },
                    new PayGrade { PayGradeName = "O-10" },
                }
            },

            //Office Roles - Enlisted
            new OrgRole {
                RoleOrderBy = 175,
                RoleName = "Technology Specialist",
                RoleShortName = "Tech Specialist",
                RoleType = "Office",
                RatingCode = "IT",
                SupportedPayGrades = new List<PayGrade> {
                    new PayGrade { PayGradeName = "E-3" },
                    new PayGrade { PayGradeName = "E-4" },
                    new PayGrade { PayGradeName = "E-5" },
                    new PayGrade { PayGradeName = "E-6" },
                    new PayGrade { PayGradeName = "E-7" },
                    new PayGrade { PayGradeName = "E-8" },
                    new PayGrade { PayGradeName = "E-9" },
                }
            },
            new OrgRole {
                RoleOrderBy = 176,
                RoleName = "Intelligence Specialist",
                RoleShortName = "Intel Specialist",
                RoleType = "Office",
                RatingCode = "IS",
                SupportedPayGrades = new List<PayGrade> {
                    new PayGrade { PayGradeName = "E-3" },
                    new PayGrade { PayGradeName = "E-4" },
                    new PayGrade { PayGradeName = "E-5" },
                    new PayGrade { PayGradeName = "E-6" },
                    new PayGrade { PayGradeName = "E-7" },
                    new PayGrade { PayGradeName = "E-8" },
                    new PayGrade { PayGradeName = "E-9" },
                }
            },
            new OrgRole {
                RoleOrderBy = 177,
                RoleName = "Finance Specialist",
                RoleShortName = "Finance  Spc.",
                RoleType = "Office",
                RatingCode = "YF",
                SupportedPayGrades = new List<PayGrade> {
                    new PayGrade { PayGradeName = "E-3" },
                    new PayGrade { PayGradeName = "E-4" },
                    new PayGrade { PayGradeName = "E-5" },
                    new PayGrade { PayGradeName = "E-6" },
                    new PayGrade { PayGradeName = "E-7" },
                    new PayGrade { PayGradeName = "E-8" },
                    new PayGrade { PayGradeName = "E-9" },
                }
            },
            new OrgRole {
                RoleOrderBy = 178,
                RoleName = "Public Relations Specialist",
                RoleShortName = "PR  Specialist",
                RoleType = "Office",
                RatingCode = "MC",
                SupportedPayGrades = new List<PayGrade> {
                    new PayGrade { PayGradeName = "E-3" },
                    new PayGrade { PayGradeName = "E-4" },
                    new PayGrade { PayGradeName = "E-5" },
                    new PayGrade { PayGradeName = "E-6" },
                    new PayGrade { PayGradeName = "E-7" },
                    new PayGrade { PayGradeName = "E-8" },
                    new PayGrade { PayGradeName = "E-9" },
                }
            },
            new OrgRole {
                RoleOrderBy = 179,
                RoleName = "Diplomacy Specialist",
                RoleShortName = "Diplomacy Spc.",
                RoleType = "Office",
                RatingCode = "YD",
                SupportedPayGrades = new List<PayGrade> {
                    new PayGrade { PayGradeName = "E-3" },
                    new PayGrade { PayGradeName = "E-4" },
                    new PayGrade { PayGradeName = "E-5" },
                    new PayGrade { PayGradeName = "E-6" },
                    new PayGrade { PayGradeName = "E-7" },
                    new PayGrade { PayGradeName = "E-8" },
                    new PayGrade { PayGradeName = "E-9" },
                }
            },

        };
        
    }
}
