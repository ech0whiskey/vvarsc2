using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vvarscNET.Model.Objects.Organizations;

namespace vvarscNET.Test.Helpers.Data
{
    public static partial class SetupData
    {
        public static List<Unit> _units = new List<Unit>()
        {
            new Unit
            {
                UnitName = "Fleet Headquarters",
                ParentUnitName = null,
                UnitDesignation = "FLEETCOM",
                UnitDescription = @"Fleet Headquarters, also know as Fleet Command (FLEETCOM), is the command unit for the entire VVarMachine StarCitizen Fleet. Members of this unit and its reporting offices are responsible for the long-term strategic planning of Fleet Operations, Public Relations, coordination with other Organizations in the StarCitizen universe (including Alliance Member Fleet Organizations), and ongoing management of the Technology Resources serving the needs of VVarMachine personnel.",
                UnitCallsign = "Castle",
                UnitType = Model.Enums.UnitTypeEnum.Fleet,
                IsHidden = false,
                IsActive = true
            },

            //Naval Commands
            new Unit
            {
                UnitName = "Naval Air Forces Command",
                UnitDesignation = "NAVAIRFORCOM",
                ParentUnitName = "Fleet Headquarters",
                UnitDescription = @"Naval Air Forces Command (NAVAIRFORCOM) is the headquarters responsible for organizing and maintaining the Fleet's tactical air combat units. Officers and Senior NCOs assigned to this unit provide oversight in operational planning, mission execution, training, and ongoing management for the units under their authority. Each Air Wing within the Command is responsible for operations in a specific area of the air combat portfolio, and may include distinct methods for implementing the points listed above.",
                UnitCallsign = "Eagle",
                UnitType = Model.Enums.UnitTypeEnum.Command,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Naval Logistics Command",
                UnitDesignation = "NAVLOGCOM",
                ParentUnitName = "Fleet Headquarters",
                UnitDescription = @"Naval Logistics Command (NAVLOGCOM) is the headquarters organizational unit for the Fleet's economic and combat support forces. It is responsible for planning and executing missions in commercial trade, mining, repair, salvage, exploration, and other critical roles to maintain our financial liquidity and ensure our Fleet can conduct future combat operations throughout the 'verse.",
                UnitCallsign = "Chronos",
                UnitType = Model.Enums.UnitTypeEnum.Command,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Marine Forces Command",
                UnitDesignation = "MARFORCOM",
                ParentUnitName = "Fleet Headquarters",
                UnitDescription = @"Marine Forces Command (MARFORCOM) is the highest level of leadership for all Marine units of the VVarMachine Fleet. It is commanded by a Marine Colonel or Brigadier General, who is assisted by other Senior Officers and Senior NCOs. Together, they supervise the officers and enlisted personnel below them, and are responsible for oversight of all aspects of operational planning/execution, training, recruitment, discipline, and management of our Marines.",
                UnitCallsign = "Ares",
                UnitType = Model.Enums.UnitTypeEnum.Command,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Naval Special Warfare Command",
                UnitDesignation = "NAVSPECWARCOM",
                ParentUnitName = "Fleet Headquarters",
                UnitDescription = @"Naval Special Warfare Command (NAVSPECWARCOM) was built to ensure the success of primary combat forces by means of the development and specialization of their supporting units, performing unconventional roles across the full range of military operations. Special Operations Capable (SOC) forces are not only those that conduct ""black"" missions or direct-action raids. They are front-line combat units who are relied upon by our Fleet's leadership to gather intelligence about the enemy, hinder their ability to respond, and work continually to test tactics and develop new SOPs for the remainder of the Fleet's forces to adopt.",
                UnitCallsign = "Chaos",
                UnitType = Model.Enums.UnitTypeEnum.Command,
                IsHidden = false,
                IsActive = true
            },

            //Air Wings
            new Unit
            {
                UnitName = "Fighter Wing 1",
                UnitDesignation = "1FW",
                ParentUnitName = "Naval Air Forces Command",
                UnitDescription = @"Fighter Wings are established to provide their Air Warfare Command with a organized leadership element for a collection of Fighter Squadrons. A Fighter Wing's subordinate squadrons are consistently responsible for performing the following roles during Fleet Operations:\r\n
- Space superiority for expeditionary combat operations\r\n
- Combat Air Patrol (CAP) of both secured and unsecured locations, including border protection\r\n
- Agile combat support for ComTrans, CSAR, EWAR, and strike units\r\n
- Escort and protection of various larger vessels",
                UnitCallsign = "Spartan",
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Strike Wing 2",
                UnitDesignation = "2SW",
                ParentUnitName = "Naval Air Forces Command",
                UnitDescription = @"Strike Wing 2 was initially formed as a long-range bomber command to target Vanduul capital ships in the defense of Leir. In the recent years, based on the changing nature of the campaign, it has been expanded to include units in additional capabilities, primarily focused around support of various ground forces of the VVarMachine combined fleet. Units of Strike Wing 2 operate cutting-edge military spacecraft designed to punch above their weight class. Its versatile equipment manifest includes both carrier-based and land-based strike fighters, and heavy bomber craft of all sizes.",
                UnitCallsign = null,
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Special Operations Air Wing 3",
                UnitDesignation = "3SOAW",
                ParentUnitName = "Naval Special Warfare Command",
                UnitDescription = @"Units of Special Operations Air Wing 3 (SOAW 3) perform front-line combat tasking that falls outside the normal operating protocols of primary air forces. SOAW 3's Squadons are tasked with missions for Reconnaissance, Electronic Warfare, and direct-action raids against high value enemy targets. Although SOAW 3 is managed independently from primary air combat squadrons, it utilizes many of the same qualifications and evaluations processes for its members in different roles. In some cases, the requirements are much more diverse for squadrons under SOAW 3 when compared to the squadrons of NAVAIRFORCOM.",
                UnitCallsign = "Atlas",
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Combat Support Wing 4",
                UnitDesignation = "4CSW",
                ParentUnitName = "Naval Air Forces Command",
                UnitDescription = @"Units of CSW 4 have been relied upon by primary air and ground forces in nearly every combat operation ever carried out by the fleet. These squadrons perform critical functions such as Combat Transport, and Combat Search & Rescue, where the primary responsibility of the unit is focused away from engaging and destroying enemy forces. While these units may not rack-up the kill count of primary combat forces, they provide an essential ingredient for mission success.",
                UnitCallsign = "Odin",
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Air Mobility Wing 5",
                UnitDesignation = "5AMW",
                ParentUnitName = "Naval Logistics Command",
                UnitDescription = @"AMW 5 is comprised of units tasked with transportation of finished goods and supplies for our combat forces. Its Squadrons are constructed around dedicated, high-efficiency freighters of various sizes, categorized into two classifications by cargo capacity: Standard Airlift & Heavy Airlift.",
                UnitCallsign = "Nitro",
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "Air Resources Wing 6",
                UnitDesignation = "6ARW",
                ParentUnitName = "Naval Logistics Command",
                UnitDescription = @"ARW 6 was established with the focus of locating, extracting, and processing valuable natural resources. Every economic opportunity begins with discovery, and that discovery brings efforts to capitalize on newly found potential. With minimal logistical (transport) assistance, this Air Wing is capable of self-sufficient planning and execution of operations to obtain the varied resources on which our fleet depends.",
                UnitCallsign = "Blackjack",
                UnitType = Model.Enums.UnitTypeEnum.Wing,
                IsHidden = false,
                IsActive = true
            },

            //Combat Teams
            new Unit
            {
                UnitName = "Infantry Combat Team 1",
                UnitDesignation = "ICT1",
                ParentUnitName = "Marine Forces Command",
                UnitDescription = @"Established in 2946, ICT 1 is the first of VVarMachine's multi-purpose organizational units of Marine Operators. Members of this unit and its child Platoons are primary front line forces, and are also trained in a wide variety of roles in unconventional and asymmetric warfare such as long range marksmanship, forward air control, close quarters combat, urban warfare, personnel extraction, demolition, and more. ICT 1 serves as MARFORCOM's primary assault force for missions requiring incursions into planetary or in-orbit installations. Additionally, in the absence of active tasking, ICT 1 is seen providing on-ship security for Command Elements and various commercial operations of Naval Logistics Command.",
                UnitCallsign = "Thunder",
                UnitType = Model.Enums.UnitTypeEnum.Team,
                IsHidden = false,
                IsActive = true
            },

            //Squadrons
            new Unit
            {
                UnitName = "112th Tactical Fighter Squadron",
                UnitDesignation = "VF-112",
                ParentUnitName = "Fighter Wing 1",
                UnitDescription = @"The 112th Fighter Squadron, or ""The Specter Knights"" as it is known within NAVAIRFORCOM, is one of VVarMachine's Space Superiority Squadrons. As part of the 1st Fighter Wing, the 112th is tasked with Space Warfare, Air Supremacy, and Agile Combat Support (ACS). Due to their wide range of capabilities, the 112th and her sister squadrons play a key role in all major combat operations. In addition to its primary responsibility as an Expeditionary Fighter Unit, the 112th may also be tasked with escorting corporate assets, providing perimeter and proximity security during mining operations, regular combat patrols, and securing the 142nd Combat Transport Squadron's Rapid Interstellar Mobility efforts. Whether a scalpel or a sledgehammer is needed, the 112th is sure to get the job done.",
                UnitCallsign = "Specter",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "114th Fighter Interceptor Squadron",
                UnitDesignation = "VF-114",
                ParentUnitName = "Fighter Wing 1",
                UnitDescription = @"Lightning speed and agility are the first things that come to mind for the 114th Fighter Interceptor Squadron. No other unit within NAVAIRFORCOM can be activated and directed on target as quickly as the 114th. Tasked with Interceptor roles where controlling borders is essential, the 114th can be airborne and on station before other units are even climbing into their cockpits, eliminating targets with pinpoint accuracy and efficiency not capable by most other fighter squadrons. Additionally, the 114th performs regular combat patrols, escort missions, and is usually the first of VVarMachine's squadrons sortied for defense against Vanduul raids in and around the Leir system.",
                UnitCallsign = "Reaper",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "117th Heavy Fighter Squadron",
                UnitDesignation = "VF-117",
                ParentUnitName = "Fighter Wing 1",
                UnitDescription = @"The 117th Heavy Fighter Squadron is the trebuchet of VVarMachine's Naval Air Forces. Tasked with extended duration missions, the 117th is the longest reaching squadron in NAVAIRFORCOM, and can operate without support from capital vessels to strike deep into enemy lines. The key roles of this squadron include escorting commercial assets on routes where refueling is not an option, and long range CAP missions in which heavy firepower is essential to area denial before standard fighter operations can arrive.",
                UnitCallsign = "Guardian",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "121st Bomber Squadron",
                UnitDesignation = "VA-121",
                ParentUnitName = "Strike Wing 2",
                UnitDescription = @"The 121st Bomber Squadron specializes in extended-range, high-damage, surgical strikes against large enemy vessels and installations. This unit operates traditional heavy bomber equipment and relies on support from 1st Fighter Wing units to get into its Area of Operations, deliver its ordinance on-target, and get back home safely. The 121st has served with distinction against numerous Vanduul battle groups during VVarMachine's ongoing campaign surrounding the Leir system, earning multiple capital-ship kills to its name.",
                UnitCallsign = "Hammer",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "123rd Attack Squadron",
                UnitDesignation = "VFA-123",
                ParentUnitName = "Strike Wing 2",
                UnitDescription = @"When the job requires putting heavy ordinance danger-close to Marines on the ground or in space, the 123rd is the unit to call. Operating the latest in carrier-based and land-based attack craft, the 123rd performs Close-Air-Support for troops deployed in a multitude of operational environments and scenarios. Unlike the 121st and other heavy bombardment squadrons, the 123rd conducts most of its operations in direct support of Marine forces, working closely with their Forward Air Controllers to suppress enemy ground forces and neutralize their defenses. Additionally, this Squadron may also be tasked as a supplemental strike force against medium size naval vessels including Corvettes, Frigates, and Transports during expeditionary and point defense operations.",
                UnitCallsign = "Madman",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "141st Rescue Squadron",
                UnitDesignation = "VCM-141",
                ParentUnitName = "Combat Support Wing 4",
                UnitDescription = @"The 141st Rescue Squadron specializes in terrestrial and interstellar Combat Search and Rescue (CSAR) missions in support of various operations of the VVarMachine Fleet, and will generally sortie all-purpose Rescue Flights centered around the highly capable Drake Cutlass Red Ambulance. In the absence of active tasking orders, the 141st is on constant standby for deep-space rescue to support the ongoing needs of all VVAR forces.",
                UnitCallsign = "Savior",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "142nd Combat Transport Squadron",
                UnitDesignation = "VC-142",
                ParentUnitName = "Combat Support Wing 4",
                UnitDescription = @"The 142nd Combat Transport Squadron focuses on bringing the troops and the tools needed to win the fight. Whether it is on a space station controlled by outlaws, on a capital ship in the middle of a fleet battle, or on a distant planet, this unit can be relied upon to get what we need, where we need it, when we need it. In addition to transporting troops, supplies, and vehicles for combat operations, this unit also provides on-demand fire support to Marine forces during insertions and extractions. This unit consistently works closely with other VVAR Military Units towards the success of the mission at-hand. This profession requires the most cool headed and daring pilots since the costs of failure are too high to incur.",
                UnitCallsign = "Blackwall",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "131st Reconnaissance Squadron",
                UnitDesignation = "VFR-131",
                ParentUnitName = "Special Operations Air Wing 3",
                UnitDescription = @"131st Reconnaissance Squadron is one of VVarMachine's Special-Operations Capable forces that provides essential elements of military intelligence to the command element of Naval Special Warfare Command (NAVSPECWARCOM); supporting task force commanders and their subordinate operating units of the Fleet Headquarters (FLEETCOM). 131st Reconnaissance Squadron conducts deep-space, long-range reconnaissance (GreenOps) and direct action raids (BlackOps) in support of VVarMachine's Military Division across the range of military operations including crisis response, expeditionary operations and major combat operations. The unit directly enhances the capabilities of primary air and ground forces through intelligence gathering, target designation, and precision strikes while utilizing methods of airborne, orbital, interstellar, and EVA insertions and extractions, similar to those of the UEE Marine Hell-Jumpers and Alliance Special Forces.",
                UnitCallsign = "Dagger",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "134th Electronic Attack Squadron",
                UnitDesignation = "VAQ-134",
                ParentUnitName = "Special Operations Air Wing 3",
                UnitDescription = @"The 134th Electronic Attack Squadron (134 EWAR) conducts operations aimed at removing the enemy's ability to ""fight smart"". Its pilots utilize a combination of deception tactics and anti-electronics technology to neutralize enemy sensors and defenses prior to a larger assault, with the goal of minimizing the loss of VVAR's primary combat forces. Primary tasking for this unit also includes conventional attacks against medium and large enemy spacecraft, Suppression of Enemy Air Defenses (SEAD), and Close Air Support. Additionally, the 134th is seen assisting the 131st Reconnaissance Squadron by operating the Drake Herald and Saber Raven for high-speed transfer of critical military intelligence from the front lines back to Command and Fleet HQ personnel during extended duration deep-space recon missions.",
                UnitCallsign = "Neptune",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "153rd Airlift Squadron",
                UnitDesignation = "VL-153",
                ParentUnitName = "Air Mobility Wing 5",
                UnitDescription = @"The 153d Airlift Squadron, activated Oct 8, 2947, is tasked with cargo and personnel transport on ships with less than 1000 SCU cargo capacity. Smaller cargo loads often imply more remote, valuable, urgent, or dangerous shipments than on larger freighters. This squadron’s personnel take pride in taking whatever, whenever, wherever, no matter the risk.",
                UnitCallsign = "Clipper",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "158th Heavy Airlift Squadron",
                UnitDesignation = "VL-158",
                ParentUnitName = "Air Mobility Wing 5",
                UnitDescription = @"The 158th Heavy Airlift Squadron, activated Oct 8, 2947, is the powerhouse behind any massive project; flying airframes that dwarf small stations, farther than thought possible every time. Capital ship delivery, station resupply, trade goods, and moving troops across galaxies with minimal stops are only some of the essential in-demand jobs this squadron performs in support of our Fleet.",
                UnitCallsign = "Husky",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "160th Mining Squadron",
                UnitDesignation = "VU-160",
                ParentUnitName = "Air Resources Wing 6",
                UnitDescription = @"The 160th Mining Squadron, activated 8 October 2947, is the backbone of VVarMachine’s economy. Operating craft big and small, drilling asteroids from the center of the Sol system, to the edges of the known universe, near the UEE or pirates and aliens, but always looking for the most valuable resources. With the dangers present, no miner is a civilian.",
                UnitCallsign = null,
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "162nd Exploration Squadron",
                UnitDesignation = "VP-162",
                ParentUnitName = "Air Resources Wing 6",
                UnitDescription = @"The 162nd Exploration Squadron, activated 8 October 2947, is composed of the exemplary troops mapping the galaxy. Its primary standing mission is to identify and report on new economic opportunities for the fleet. Heading places rarely trekked, explorers can also provide valuable information to create new charts and flight paths, and test existing paths for our fleet to traverse. While not tasked with infiltration, explorers still must expect combat and be properly trained beyond normal expectations in defense and escape, with the potential for no support to ever come.",
                UnitCallsign = "Patriot",
                UnitType = Model.Enums.UnitTypeEnum.Squadron,
                IsHidden = false,
                IsActive = true
            },

            //Marine Platoons
            new Unit
            {
                UnitName = "1st Platoon",
                UnitFullName = "1st Platoon, Infantry Combat Team 1",
                UnitDesignation = "ICT1-1",
                ParentUnitName = "Infantry Combat Team 1",
                UnitDescription = null,
                UnitCallsign = "Thunder 1",
                UnitType = Model.Enums.UnitTypeEnum.Platoon,
                IsHidden = false,
                IsActive = true
            },
            new Unit
            {
                UnitName = "2nd Platoon",
                UnitFullName = "2nd Platoon, Infantry Combat Team 1",
                UnitDesignation = "ICT1-2",
                ParentUnitName = "Infantry Combat Team 1",
                UnitDescription = null,
                UnitCallsign = "Thunder 2",
                UnitType = Model.Enums.UnitTypeEnum.Platoon,
                IsHidden = false,
                IsActive = true
            },
            
        };

    }
}
