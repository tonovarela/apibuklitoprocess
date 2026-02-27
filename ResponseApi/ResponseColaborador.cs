
namespace apiBukLitoprocess.Clases;

 public partial class ResponseColaborador
    {
        public Data? data { get; set; }
    }

    public partial class Data
    {
        public long? person_id { get; set; }
        public long? id { get; set; }
        public Uri picture_url { get; set; }
        public string first_name { get; set; }
        public string surname { get; set; }
        public string second_surname { get; set; }
        public string full_name { get; set; }
        public string document_type { get; set; }
        public string document_number { get; set; }
        public string curp { get; set; }
        public string rfc { get; set; }
        public string code_sheet { get; set; }
        public string social_security { get; set; }
        public string gender { get; set; }
        public DateTimeOffset? birthday { get; set; }
        public string nationality { get; set; }
        public string civil_status { get; set; }
        public object office_phone { get; set; }
        public long? celular { get; set; }
        public long? phone { get; set; }
        public string email { get; set; }
        public string personal_email { get; set; }
        public string address { get; set; }
        public long? location_id { get; set; }
        public long? postal_code { get; set; }
        public DateTimeOffset? active_since { get; set; }
        public string status { get; set; }
        public string payment_method { get; set; }
        public object bank { get; set; }
        public object account_number { get; set; }
        public object clabe { get; set; }
        public object client_number { get; set; }
        public string country_code { get; set; }
        public bool? private_role { get; set; }
        public string health_company { get; set; }
        public string pension_regime { get; set; }
        public object pension_fund { get; set; }
        public object afc { get; set; }
        public bool? retired { get; set; }
        public object retirement_regime { get; set; }
        public string payment_currency { get; set; }
        public string period_type { get; set; }
        public DataCustomAttributes custom_attributes { get; set; }
        public object active_until { get; set; }
        public object termination_reason { get; set; }
        public CurrentJob current_job { get; set; }
        public List<FamilyResponsability> family_responsabilities { get; set; }
    }

    public partial class CurrentJob
    {
        public string periodicity { get; set; }
        public string frequency { get; set; }
        public string working_schedule_type { get; set; }
        public bool? zone_assignment { get; set; }
        public object union { get; set; }
        public object project { get; set; }
        public List<string> days { get; set; }
        public object previous_job_id { get; set; }
        public long? id { get; set; }
        public long? company_id { get; set; }
        public DateTimeOffset? start_date { get; set; }
        public object end_date { get; set; }
        public long? area_id { get; set; }
        public string cost_center { get; set; }
        public object active_until { get; set; }
        public string currency_code { get; set; }
        public Boss boss { get; set; }
        public long? employee_id { get; set; }
        public double? wage { get; set; }
        public string type_of_regime { get; set; }
        public string type_of_contract { get; set; }
        public object end_of_contract { get; set; }
        public object notice_date { get; set; }
        public string type_of_working_day { get; set; }
        public long? regular_hours { get; set; }
        public object risk { get; set; }
        public string wage_type { get; set; }
        public bool? without_wage { get; set; }
        public long? location_id { get; set; }
        public object union_id { get; set; }
        public long? company_registration { get; set; }
        public string status { get; set; }
        public Role role { get; set; }
        public CurrentJobCustomAttributes custom_attributes { get; set; }
    }

    public partial class Boss
    {
        public long? id { get; set; }
        public string document_type { get; set; }
        public string document_number { get; set; }
        public string rfc { get; set; }
    }

    public partial class CurrentJobCustomAttributes
    {
        public string centroCostos { get; set; }
        public object ctrlit_recinto { get; set; }
    }

    public partial class Role
    {
        public long? id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string requirements { get; set; }
        public List<long> area_ids { get; set; }
        public object role_family { get; set; }
    }

    public partial class DataCustomAttributes
    {
        public long? idColaborador { get; set; }
        public object numInt { get; set; }
        public object numExt { get; set; }
        public object Alergias { get; set; }
        public string Beneficiario1 { get; set; }
        public object Beneficiario2 { get; set; }
        public object Beneficiario3 { get; set; }
        public string Colonia { get; set; }
        public object contactoEmergencia1 { get; set; }
        public object contactoEmergencia2 { get; set; }
        public string Delegacion { get; set; }
        public string EstadoCivil { get; set; }
        public long? Ext { get; set; }
        public DateTimeOffset? fecNacBeneficiario1 { get; set; }
        public object fecNacBeneficiario2 { get; set; }
        public object fecNacBeneficiario3 { get; set; }
        public string NivelAcademico { get; set; }
        public string Pais { get; set; }
        public string parentescoBeneficiario1 { get; set; }
        public object parentescoBeneficiario2 { get; set; }
        public object parentescoBeneficiario3 { get; set; }
        public object parentestoContactoEmerg1 { get; set; }
        public object parentestoContactoEmerg2 { get; set; }
        public long? porcBeneficiario1 { get; set; }
        public object porcBeneficiario2 { get; set; }
        public object porcBeneficiario3 { get; set; }
        public object telContactoEmerg1 { get; set; }
        public object telContactoEmerg2 { get; set; }
        public string TipoColaborador { get; set; }
        public string tipoSangre { get; set; }
    }

    public partial class FamilyResponsability
    {
        public long? id { get; set; }
        public string family_allowance_section { get; set; }
        public long? simple_family_responsability { get; set; }
        public long? maternity_family_responsability { get; set; }
        public long? invalid_family_responsability { get; set; }
        public DateTimeOffset? start_date { get; set; }
        public object end_date { get; set; }
        public List<object> responsability_details { get; set; }
    }

