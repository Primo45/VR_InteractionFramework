using UnityEngine;

/// <summary>
/// Stores the 30 simulated patient medical records (HT01-HT10, DL01-DL10, P01-P10) in English.
/// Exposes only the player-facing history and excludes physical exam measurements,
/// lab results, diagnoses, ping-pong color answers, and advice.
/// </summary>
public static class PatientRecordDatabase
{
    public class PatientRecord
    {
        public string Code;
        public string FullName;
        public string Gender;
        public string AgeText;
        public string ChiefComplaint;
        public string PresentIllness;
        public string MedicalHistory;
        public string FamilyHistory;
        public string BehaviorAndSocial;
        public string CurrentMedications;

        public string FormatPlayerHistory()
        {
            return
                $"<b>[ PATIENT MEDICAL HISTORY ]</b>\n" +
                $"<b>{Code}  {FullName}</b>  |  Gender: {Gender}  |  Age: {AgeText}\n\n" +
                $"<b>• Chief Complaint:</b> {ChiefComplaint}\n" +
                $"<b>• Present Illness:</b> {PresentIllness}\n" +
                $"<b>• Medical History:</b> {MedicalHistory}\n" +
                $"<b>• Family History:</b> {FamilyHistory}\n" +
                $"<b>• Lifestyle & Social:</b> {BehaviorAndSocial}\n" +
                $"<b>• Current Medications:</b> {CurrentMedications}";
        }
    }

    public static PatientRecord CurrentPatient { get; private set; }

    public static readonly PatientRecord[] AllPatients = new PatientRecord[]
    {
        // ================= Hypertension Cases (HT01 - HT10) =================
        new PatientRecord
        {
            Code = "HT01",
            FullName = "Mr. Sarawut Wattanachai",
            Gender = "Male",
            AgeText = "39 years",
            ChiefComplaint = "Annual company health check-up; no abnormal symptoms.",
            PresentIllness = "Never had high blood pressure readings. No headache, palpitations, chest tightness, or unusual fatigue.",
            MedicalHistory = "No underlying diseases and has never taken antihypertensive medication.",
            FamilyHistory = "Father developed hypertension after age 60.",
            BehaviorAndSocial = "Accountant; brisk walking 30 minutes 5 days/week, eats home-cooked meals, non-smoker, does not drink alcohol.",
            CurrentMedications = "No regular medications."
        },
        new PatientRecord
        {
            Code = "HT02",
            FullName = "Ms. Kanokwan Poonsap",
            Gender = "Female",
            AgeText = "34 years",
            ChiefComplaint = "Pre-employment health check-up.",
            PresentIllness = "No acute symptoms, though repeated readings on exam day were above the normal range.",
            MedicalHistory = "Never diagnosed with hypertension.",
            FamilyHistory = "Mother has hypertension and type 2 diabetes.",
            BehaviorAndSocial = "Works long hours at a computer, frequently drinks bubble tea and eats fried food, minimal exercise, non-smoker.",
            CurrentMedications = "No regular medications."
        },
        new PatientRecord
        {
            Code = "HT03",
            FullName = "Mr. Somchai Srisawat",
            Gender = "Male",
            AgeText = "52 years",
            ChiefComplaint = "Scheduled hypertension follow-up.",
            PresentIllness = "Diagnosed 3 years ago; currently asymptomatic and takes medication regularly.",
            MedicalHistory = "Hypertension with no known complications.",
            FamilyHistory = "Older brother has hypertension.",
            BehaviorAndSocial = "Commercial driver; cycles regularly, quit smoking 5 years ago, rarely drinks alcohol.",
            CurrentMedications = "Prescribed antihypertensive medication."
        },
        new PatientRecord
        {
            Code = "HT04",
            FullName = "Mrs. Kanyarat Meesuk",
            Gender = "Female",
            AgeText = "46 years",
            ChiefComplaint = "Intermittent dull occipital headache.",
            PresentIllness = "Had two high blood pressure readings at a pharmacy over the past month and came for evaluation.",
            MedicalHistory = "Never diagnosed with hypertension.",
            FamilyHistory = "Mother has hypertension and hyperlipidemia.",
            BehaviorAndSocial = "Food vendor; frequently tastes food and uses salty seasonings, rarely exercises, gets little sleep.",
            CurrentMedications = "No regular medications."
        },
        new PatientRecord
        {
            Code = "HT05",
            FullName = "Mr. Narin Tangman",
            Gender = "Male",
            AgeText = "55 years",
            ChiefComplaint = "Dizziness and medication refill after missing follow-up appointments.",
            PresentIllness = "Diagnosed with hypertension 2 years ago; works shifts and frequently forgets medication. Home blood pressure has remained high.",
            MedicalHistory = "Hypertension and obesity; no confirmed complications.",
            FamilyHistory = "Father passed away from stroke.",
            BehaviorAndSocial = "Frequently eats instant noodles and fried food, smokes 8-10 cigarettes/day, drinks beer on weekends.",
            CurrentMedications = "Prescribed antihypertensive medication, taken irregularly."
        },
        new PatientRecord
        {
            Code = "HT06",
            FullName = "Mrs. Sunee Boonruang",
            Gender = "Female",
            AgeText = "62 years",
            ChiefComplaint = "Severe headache and occasional blurred vision.",
            PresentIllness = "Has had hypertension for 8 years; stopped taking medication on and off over the past 3 months.",
            MedicalHistory = "Hypertension, dyslipidemia, and obesity; no confirmed end-organ complications.",
            FamilyHistory = "Mother had a history of stroke.",
            BehaviorAndSocial = "Market vendor; prefers pickled/fermented foods and coconut-milk curries, does not exercise, sleeps poorly.",
            CurrentMedications = "Antihypertensive and lipid-lowering medications, intermittently self-discontinued."
        },
        new PatientRecord
        {
            Code = "HT07",
            FullName = "Mr. Preecha Saengthong",
            Gender = "Male",
            AgeText = "68 years",
            ChiefComplaint = "Follow-up after ischemic stroke.",
            PresentIllness = "Had hemiparesis 8 months ago; currently walks with a cane and has mild left-arm weakness.",
            MedicalHistory = "Hypertension, diabetes, and ischemic stroke.",
            FamilyHistory = "Father had hypertension.",
            BehaviorAndSocial = "Lives with wife who helps organize medications; quit smoking after the stroke.",
            CurrentMedications = "Antihypertensive medication and secondary stroke prevention regimen."
        },
        new PatientRecord
        {
            Code = "HT08",
            FullName = "Ms. Rungnapa Chaiwat",
            Gender = "Female",
            AgeText = "32 years",
            ChiefComplaint = "Annual health check-up; gained 6 kg over the past year.",
            PresentIllness = "No abnormal symptoms, but works late nights and orders delivery food almost daily.",
            MedicalHistory = "Never diagnosed with hypertension.",
            FamilyHistory = "Father has hypertension.",
            BehaviorAndSocial = "Designer; long screen hours, drinks sweetened coffee daily, non-smoker.",
            CurrentMedications = "No regular medications."
        },
        new PatientRecord
        {
            Code = "HT09",
            FullName = "Mrs. Juthamas Pattanakit",
            Gender = "Female",
            AgeText = "57 years",
            ChiefComplaint = "Follow-up after 3 months of lifestyle modification.",
            PresentIllness = "Diagnosed with hypertension 1 year ago; currently takes medication regularly and monitors blood pressure at home.",
            MedicalHistory = "Hypertension; no major comorbidities or confirmed complications.",
            FamilyHistory = "Older sister has hypertension.",
            BehaviorAndSocial = "Retired teacher; walks in the garden every morning, cooks at home, reduces fish sauce and processed food, non-smoker.",
            CurrentMedications = "Prescribed antihypertensive medication, taken regularly."
        },
        new PatientRecord
        {
            Code = "HT10",
            FullName = "Mr. Wichai Pakdee",
            Gender = "Male",
            AgeText = "67 years",
            ChiefComplaint = "Scheduled follow-up for kidney function and blood pressure.",
            PresentIllness = "Long-standing hypertension; developed chronic kidney disease during ongoing follow-up.",
            MedicalHistory = "Hypertension, chronic kidney disease, and dyslipidemia.",
            FamilyHistory = "Mother had chronic kidney disease with hypertension.",
            BehaviorAndSocial = "Gardener; frequently eats pickled/fermented foods, former smoker (quit), wife helps manage medications.",
            CurrentMedications = "Antihypertensive medication and renal care regimen."
        },

        // ================= Dyslipidemia Cases (DL01 - DL10) =================
        new PatientRecord
        {
            Code = "DL01",
            FullName = "Mr. Krittaphat Wongsawat",
            Gender = "Male",
            AgeText = "32 years",
            ChiefComplaint = "Annual health check-up; no abnormal symptoms.",
            PresentIllness = "Never told he had high cholesterol; exercises regularly and does not smoke.",
            MedicalHistory = "No diabetes, hypertension, or cardiovascular disease.",
            FamilyHistory = "No premature heart disease in first-degree relatives.",
            BehaviorAndSocial = "Engineer; runs 3-4 days/week, eats a varied diet, drinks alcohol rarely.",
            CurrentMedications = "No regular medications."
        },
        new PatientRecord
        {
            Code = "DL02",
            FullName = "Ms. Pimchanok Janpen",
            Gender = "Female",
            AgeText = "37 years",
            ChiefComplaint = "Pre-employment check-up; slight weight gain over 2 years.",
            PresentIllness = "No specific symptoms; sits for long hours at work and drinks sugary beverages several days a week.",
            MedicalHistory = "Never diagnosed with dyslipidemia.",
            FamilyHistory = "Mother has type 2 diabetes and hyperlipidemia.",
            BehaviorAndSocial = "Computer-based office work, rarely exercises, non-smoker.",
            CurrentMedications = "No lipid-lowering medications."
        },
        new PatientRecord
        {
            Code = "DL03",
            FullName = "Mr. Thanakorn Sukjai",
            Gender = "Male",
            AgeText = "48 years",
            ChiefComplaint = "Scheduled dyslipidemia follow-up; asymptomatic.",
            PresentIllness = "Diagnosed with dyslipidemia 3 years ago; takes medication consistently.",
            MedicalHistory = "Dyslipidemia; no cardiovascular disease.",
            FamilyHistory = "Older brother has hyperlipidemia.",
            BehaviorAndSocial = "Cycles 30 minutes 5 days/week; former smoker (quit).",
            CurrentMedications = "Prescribed lipid-lowering medication."
        },
        new PatientRecord
        {
            Code = "DL04",
            FullName = "Mrs. Orathai Kaewkarn",
            Gender = "Female",
            AgeText = "45 years",
            ChiefComplaint = "Check-up after weight gain and feeling easily fatigued.",
            PresentIllness = "Previously told she had high cholesterol, but did not follow up.",
            MedicalHistory = "No known cardiovascular disease.",
            FamilyHistory = "Mother has hyperlipidemia.",
            BehaviorAndSocial = "Food vendor; frequently tastes fried foods and coconut-milk dishes, rarely exercises.",
            CurrentMedications = "None."
        },
        new PatientRecord
        {
            Code = "DL05",
            FullName = "Mr. Banphot Tangtrong",
            Gender = "Male",
            AgeText = "55 years",
            ChiefComplaint = "Follow-up after missing appointments for several months.",
            PresentIllness = "Aware of high cholesterol but takes medication irregularly due to shift work.",
            MedicalHistory = "Dyslipidemia and obesity; no confirmed complications.",
            FamilyHistory = "Father had a stroke at age 62.",
            BehaviorAndSocial = "Eats convenience/processed foods and fried items, smokes 8-10 cigarettes/day.",
            CurrentMedications = "Prescribed lipid-lowering medication, used inconsistently."
        },
        new PatientRecord
        {
            Code = "DL06",
            FullName = "Mrs. Malee Rungruang",
            Gender = "Female",
            AgeText = "61 years",
            ChiefComplaint = "Lipid follow-up after stopping medication intermittently.",
            PresentIllness = "No abdominal pain, but frequently eats sweets and coconut-milk curries.",
            MedicalHistory = "Dyslipidemia and obesity; no confirmed heart disease, stroke, or pancreatitis.",
            FamilyHistory = "Mother had a history of stroke.",
            BehaviorAndSocial = "Does not exercise, non-smoker, drinks alcohol rarely.",
            CurrentMedications = "Prescribed lipid-lowering medication, intermittently self-discontinued."
        },
        new PatientRecord
        {
            Code = "DL07",
            FullName = "Mr. Channarong Boonmee",
            Gender = "Male",
            AgeText = "68 years",
            ChiefComplaint = "Follow-up after ischemic stroke.",
            PresentIllness = "Had an ischemic stroke 8 months ago; caregiver currently helps organize medications.",
            MedicalHistory = "Dyslipidemia, hypertension, and ischemic stroke.",
            FamilyHistory = "Father passed away from heart disease.",
            BehaviorAndSocial = "Quit smoking after the stroke and tries to reduce salty and fatty foods.",
            CurrentMedications = "Prescribed lipid-lowering and vascular protective medications."
        },
        new PatientRecord
        {
            Code = "DL08",
            FullName = "Ms. Nichaphat Saengdao",
            Gender = "Female",
            AgeText = "32 years",
            ChiefComplaint = "Annual health check-up; gained 6 kg in 1 year.",
            PresentIllness = "Works late nights and eats delivery food almost every day.",
            MedicalHistory = "Never diagnosed with dyslipidemia.",
            FamilyHistory = "Father has hyperlipidemia.",
            BehaviorAndSocial = "Long screen hours, drinks sweetened coffee daily, non-smoker.",
            CurrentMedications = "None."
        },
        new PatientRecord
        {
            Code = "DL09",
            FullName = "Mrs. Wanna Srisuk",
            Gender = "Female",
            AgeText = "57 years",
            ChiefComplaint = "Follow-up after 3 months of dietary adjustment and regular medication.",
            PresentIllness = "Keeps a food log and exercises consistently.",
            MedicalHistory = "Dyslipidemia and type 2 diabetes; no confirmed ASCVD.",
            FamilyHistory = "Older sister has diabetes and hyperlipidemia.",
            BehaviorAndSocial = "Walks every morning, cooks at home, non-smoker, does not drink alcohol.",
            CurrentMedications = "Prescribed lipid-lowering and diabetes medications."
        },
        new PatientRecord
        {
            Code = "DL10",
            FullName = "Mr. Prasert Inthong",
            Gender = "Male",
            AgeText = "67 years",
            ChiefComplaint = "Follow-up after prior myocardial infarction.",
            PresentIllness = "Hospitalized for myocardial infarction 2 years ago.",
            MedicalHistory = "Dyslipidemia, coronary artery disease, and chronic kidney disease.",
            FamilyHistory = "Mother had heart disease and diabetes.",
            BehaviorAndSocial = "Former smoker (quit); wife helps organize medications.",
            CurrentMedications = "Prescribed lipid-lowering and cardiac medications."
        },

        // ================= Diabetes Cases (P01 - P10) =================
        new PatientRecord
        {
            Code = "P01",
            FullName = "Master Krittaphat Wenu",
            Gender = "Male",
            AgeText = "11 years",
            ChiefComplaint = "Annual school health screening.",
            PresentIllness = "Generally healthy; no excessive thirst, frequent urination, weight loss, or fatigue.",
            MedicalHistory = "No underlying diseases, drug allergies, or chronic illnesses.",
            FamilyHistory = "Father developed type 2 diabetes at age 45; mother is healthy; paternal grandfather has hypertension.",
            BehaviorAndSocial = "Eats 3 balanced meals/day, occasionally has snacks and soda, plays actively with friends at school daily.",
            CurrentMedications = "No regular medications."
        },
        new PatientRecord
        {
            Code = "P02",
            FullName = "Miss Miranya Suphawet",
            Gender = "Female",
            AgeText = "12 years",
            ChiefComplaint = "Type 1 diabetes follow-up.",
            PresentIllness = "Diagnosed with type 1 diabetes at age 10 after polyuria, polydipsia, and weight loss. Currently uses basal-bolus insulin regularly, checks fingerstick glucose as planned, and has had no severe hypoglycemia recently.",
            MedicalHistory = "Type 1 diabetes for 2 years; no recurrent DKA or history of severe hypoglycemia.",
            FamilyHistory = "No type 1 diabetes in family; maternal grandmother has type 2 diabetes.",
            BehaviorAndSocial = "Lives with mother, follows dietitian meal plan, swims 3 times/week, checks fingerstick glucose 4 times/day.",
            CurrentMedications = "Insulin glargine and insulin aspart as prescribed."
        },
        new PatientRecord
        {
            Code = "P03",
            FullName = "Mr. Thanawin Kalison",
            Gender = "Male",
            AgeText = "22 years",
            ChiefComplaint = "Post-stroke follow-up with uncontrolled blood sugar and blood pressure.",
            PresentIllness = "Type 2 diabetes since age 17 and hypertension since age 19; missed appointments and took medications irregularly. Had a left MCA ischemic stroke 1 year ago; currently has mild right-sided weakness (MRC 4) and mild expressive aphasia.",
            MedicalHistory = "Type 2 diabetes, hypertension, ischemic stroke, CKD stage 3, dyslipidemia, and class II obesity.",
            FamilyHistory = "Father died of myocardial infarction at age 50; mother has diabetes and hypertension.",
            BehaviorAndSocial = "Smokes ~1 pack/day since age 15, drinks alcohol occasionally, factory worker, does not exercise.",
            CurrentMedications = "Metformin, insulin glargine, enalapril, amlodipine, atorvastatin, and aspirin as prescribed."
        },
        new PatientRecord
        {
            Code = "P04",
            FullName = "Ms. Pimrada Waranon",
            Gender = "Female",
            AgeText = "23 years",
            ChiefComplaint = "Elevated blood sugar noted during annual health check-up.",
            PresentIllness = "Asymptomatic and has not started any medication; visiting for follow-up after annual check-up.",
            MedicalHistory = "No underlying diseases or prior hospitalizations.",
            FamilyHistory = "Mother and maternal aunt have type 2 diabetes.",
            BehaviorAndSocial = "Office worker, mostly sedentary, irregular exercise, often eats out, drinks alcohol socially 1-2 times/month, non-smoker.",
            CurrentMedications = "None."
        },
        new PatientRecord
        {
            Code = "P05",
            FullName = "Mr. Akirameth Naruphat",
            Gender = "Male",
            AgeText = "31 years",
            ChiefComplaint = "Scheduled visit with uncontrolled diabetes and hypertension.",
            PresentIllness = "Has had type 2 diabetes for 3 years and hypertension for 2 years; takes medication inconsistently when feeling well. Diet high in carbs and sugar; drinks 2-3 cans of beer/day 3-4 days/week.",
            MedicalHistory = "Poorly controlled diabetes and hypertension, dyslipidemia, class II obesity, and suspected obstructive sleep apnea (unconfirmed).",
            FamilyHistory = "Father had diabetes, hypertension, and CKD before dying from ESRD at age 60.",
            BehaviorAndSocial = "Truck driver, spends most of the day sitting, eats street food, no exercise, smokes ~half pack/day.",
            CurrentMedications = "Metformin, glibenclamide, enalapril, hydrochlorothiazide, and simvastatin as prescribed."
        },
        new PatientRecord
        {
            Code = "P06",
            FullName = "Ms. Rinthira Jantharawen",
            Gender = "Female",
            AgeText = "29 years",
            ChiefComplaint = "Diabetes follow-up; blood sugar remains above target.",
            PresentIllness = "Type 2 diabetes for 2 years and hypertension for 1 year; takes medication most days but occasionally forgets. Eats large portions of rice and exercises little due to shift work.",
            MedicalHistory = "Type 2 diabetes, hypertension, and class I obesity.",
            FamilyHistory = "Mother has diabetes and hypertension; older sister developed diabetes at age 35.",
            BehaviorAndSocial = "Factory shift worker (alternating morning and night shifts); main diet is rice with curry; non-smoker, no alcohol.",
            CurrentMedications = "Metformin, glipizide, and enalapril as prescribed."
        },
        new PatientRecord
        {
            Code = "P07",
            FullName = "Mrs. Sasithara Waranyu",
            Gender = "Female",
            AgeText = "41 years",
            ChiefComplaint = "Follow-up for diabetes and hypertension after starting treatment.",
            PresentIllness = "Type 2 diabetes for 1 year and hypertension for 6 months; lost 3 kg over 6 months after starting medication and adjusting diet, with improved control.",
            MedicalHistory = "Type 2 diabetes, hypertension, and class I obesity.",
            FamilyHistory = "Mother has diabetes and hypertension; older brother has diabetes.",
            BehaviorAndSocial = "Cooks at home reducing sugar, fat, and salt; walks 30 minutes 4-5 days/week; motivated in self-care.",
            CurrentMedications = "Metformin and amlodipine as prescribed."
        },
        new PatientRecord
        {
            Code = "P08",
            FullName = "Mr. Chanathip Rattanawetha",
            Gender = "Male",
            AgeText = "35 years",
            ChiefComplaint = "Elevated blood sugar and blood pressure noted at company health check-up.",
            PresentIllness = "Here for follow-up after company check-up; no abnormal symptoms.",
            MedicalHistory = "No known chronic conditions.",
            FamilyHistory = "Father died of myocardial infarction at age 55; mother has hypertension; older sister has type 2 diabetes.",
            BehaviorAndSocial = "Accountant sitting 8-10 hours/day, drinks sweetened coffee every morning, no exercise, non-smoker, drinks beer 1-2 times/week.",
            CurrentMedications = "None."
        },
        new PatientRecord
        {
            Code = "P09",
            FullName = "Mrs. Oramanat Sirawadee",
            Gender = "Female",
            AgeText = "45 years",
            ChiefComplaint = "Uncontrolled diabetes and hypertension with worsening blurred vision.",
            PresentIllness = "Type 2 diabetes for 8 years and hypertension for 10 years; missed appointments and took medications irregularly. Diagnosed with bilateral non-proliferative diabetic retinopathy and microalbuminuria.",
            MedicalHistory = "Poorly controlled diabetes and hypertension, diabetic retinopathy, early diabetic nephropathy, class II obesity, and dyslipidemia.",
            FamilyHistory = "Mother died of diabetes-related ESRD.",
            BehaviorAndSocial = "Made-to-order food vendor tasting food throughout the day (high fat, sugar, and sodium); no time to exercise; non-smoker, no alcohol.",
            CurrentMedications = "Metformin, insulin glargine, enalapril, amlodipine, and atorvastatin as prescribed."
        },
        new PatientRecord
        {
            Code = "P10",
            FullName = "Miss Lalinthara Wetchawarin",
            Gender = "Female",
            AgeText = "10 years",
            ChiefComplaint = "Follow-up for childhood obesity, elevated blood sugar, and pediatric hypertension.",
            PresentIllness = "Childhood obesity with acanthosis nigricans and symptoms of increased thirst and frequent urination.",
            MedicalHistory = "Childhood obesity, early type 2 diabetes, pediatric hypertension, acanthosis nigricans, and dyslipidemia.",
            FamilyHistory = "Father has type 2 diabetes, hypertension, and obesity; mother has hypertension and overweight.",
            BehaviorAndSocial = "Family diet high in fat, sugar, and starch; drinks 1-2 cans of soda/day; no exercise; 4-5 hours/day screen time.",
            CurrentMedications = "Metformin and amlodipine per pediatric plan."
        }
    };

    /// <summary>
    /// Selects and stores one random patient from all 30 cases.
    /// </summary>
    public static PatientRecord SelectRandomPatient()
    {
        int index = Random.Range(0, AllPatients.Length);
        CurrentPatient = AllPatients[index];
        return CurrentPatient;
    }
}
