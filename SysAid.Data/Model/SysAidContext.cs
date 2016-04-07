namespace SysAid.Data.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SysAidContext : DbContext
    {
        public SysAidContext()
            : base("name=SysAidContext")
        {
        }

        public virtual DbSet<account> accounts { get; set; }
        public virtual DbSet<agreement> agreements { get; set; }
        public virtual DbSet<asset_catalog> asset_catalog { get; set; }
        public virtual DbSet<asset_catalog_files> asset_catalog_files { get; set; }
        public virtual DbSet<asset_catalog_history> asset_catalog_history { get; set; }
        public virtual DbSet<asset_catalog_links> asset_catalog_links { get; set; }
        public virtual DbSet<asset_data_day_data> asset_data_day_data { get; set; }
        public virtual DbSet<asset_data_month_data> asset_data_month_data { get; set; }
        public virtual DbSet<asset_data_week_data> asset_data_week_data { get; set; }
        public virtual DbSet<asset_data_year_data> asset_data_year_data { get; set; }
        public virtual DbSet<asset_notif_events> asset_notif_events { get; set; }
        public virtual DbSet<asset_offline_log> asset_offline_log { get; set; }
        public virtual DbSet<asset_types> asset_types { get; set; }
        public virtual DbSet<asset2ci> asset2ci { get; set; }
        public virtual DbSet<audit_log> audit_log { get; set; }
        public virtual DbSet<audit_log_lines> audit_log_lines { get; set; }
        public virtual DbSet<automatic_texts> automatic_texts { get; set; }
        public virtual DbSet<chat_active_sessions> chat_active_sessions { get; set; }
        public virtual DbSet<chat_closed_sessions> chat_closed_sessions { get; set; }
        public virtual DbSet<chat_queue> chat_queue { get; set; }
        public virtual DbSet<chat_queue_messages> chat_queue_messages { get; set; }
        public virtual DbSet<ci_attributes> ci_attributes { get; set; }
        public virtual DbSet<ci_files> ci_files { get; set; }
        public virtual DbSet<ci_history> ci_history { get; set; }
        public virtual DbSet<ci_links> ci_links { get; set; }
        public virtual DbSet<ci_relation> ci_relation { get; set; }
        public virtual DbSet<ci_relation_type> ci_relation_type { get; set; }
        public virtual DbSet<ci_sub_type> ci_sub_type { get; set; }
        public virtual DbSet<ci_template> ci_template { get; set; }
        public virtual DbSet<ci_template_links> ci_template_links { get; set; }
        public virtual DbSet<ci_type> ci_type { get; set; }
        public virtual DbSet<command> commands { get; set; }
        public virtual DbSet<comp_update_day_data> comp_update_day_data { get; set; }
        public virtual DbSet<comp_update_month_data> comp_update_month_data { get; set; }
        public virtual DbSet<comp_update_week_data> comp_update_week_data { get; set; }
        public virtual DbSet<comp_update_year_data> comp_update_year_data { get; set; }
        public virtual DbSet<company> companies { get; set; }
        public virtual DbSet<company_files> company_files { get; set; }
        public virtual DbSet<company_history> company_history { get; set; }
        public virtual DbSet<company_links> company_links { get; set; }
        public virtual DbSet<computer> computers { get; set; }
        public virtual DbSet<computer_attributes> computer_attributes { get; set; }
        public virtual DbSet<computer_attributes_history> computer_attributes_history { get; set; }
        public virtual DbSet<computer_files> computer_files { get; set; }
        public virtual DbSet<computer_group> computer_group { get; set; }
        public virtual DbSet<computer_history> computer_history { get; set; }
        public virtual DbSet<computer_links> computer_links { get; set; }
        public virtual DbSet<computer_log> computer_log { get; set; }
        public virtual DbSet<computer_patches> computer_patches { get; set; }
        public virtual DbSet<computer_users> computer_users { get; set; }
        public virtual DbSet<current_measurement_lists> current_measurement_lists { get; set; }
        public virtual DbSet<current_sla_results> current_sla_results { get; set; }
        public virtual DbSet<cust_values> cust_values { get; set; }
        public virtual DbSet<custom_columns> custom_columns { get; set; }
        public virtual DbSet<custom_triggers> custom_triggers { get; set; }
        public virtual DbSet<customized_day_data> customized_day_data { get; set; }
        public virtual DbSet<customized_month_data> customized_month_data { get; set; }
        public virtual DbSet<customized_snmp_oids> customized_snmp_oids { get; set; }
        public virtual DbSet<customized_week_data> customized_week_data { get; set; }
        public virtual DbSet<customized_year_data> customized_year_data { get; set; }
        public virtual DbSet<discovery_service> discovery_service { get; set; }
        public virtual DbSet<_event> events { get; set; }
        public virtual DbSet<faq> faqs { get; set; }
        public virtual DbSet<faq_files> faq_files { get; set; }
        public virtual DbSet<generic_messages> generic_messages { get; set; }
        public virtual DbSet<gfi_products> gfi_products { get; set; }
        public virtual DbSet<last_run_measurement_lists> last_run_measurement_lists { get; set; }
        public virtual DbSet<linked_service_req> linked_service_req { get; set; }
        public virtual DbSet<list_view> list_view { get; set; }
        public virtual DbSet<mdm_policy> mdm_policy { get; set; }
        public virtual DbSet<mdm_wifi_policy> mdm_wifi_policy { get; set; }
        public virtual DbSet<measurements_def> measurements_def { get; set; }
        public virtual DbSet<measurements_def_history> measurements_def_history { get; set; }
        public virtual DbSet<measurements_lists> measurements_lists { get; set; }
        public virtual DbSet<measurements_lists_history> measurements_lists_history { get; set; }
        public virtual DbSet<message> messages { get; set; }
        public virtual DbSet<monitor_embed_data> monitor_embed_data { get; set; }
        public virtual DbSet<monitor_events> monitor_events { get; set; }
        public virtual DbSet<monitor_templates> monitor_templates { get; set; }
        public virtual DbSet<monitoring_conf> monitoring_conf { get; set; }
        public virtual DbSet<network_activity_day_data> network_activity_day_data { get; set; }
        public virtual DbSet<network_activity_month_data> network_activity_month_data { get; set; }
        public virtual DbSet<network_activity_week_data> network_activity_week_data { get; set; }
        public virtual DbSet<network_activity_year_data> network_activity_year_data { get; set; }
        public virtual DbSet<network_day_data> network_day_data { get; set; }
        public virtual DbSet<network_month_data> network_month_data { get; set; }
        public virtual DbSet<network_week_data> network_week_data { get; set; }
        public virtual DbSet<network_year_data> network_year_data { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<online_assets> online_assets { get; set; }
        public virtual DbSet<online_users> online_users { get; set; }
        public virtual DbSet<online_users_history> online_users_history { get; set; }
        public virtual DbSet<patch> patches { get; set; }
        public virtual DbSet<patch_policy> patch_policy { get; set; }
        public virtual DbSet<patch_policy_change> patch_policy_change { get; set; }
        public virtual DbSet<patch_policy_event> patch_policy_event { get; set; }
        public virtual DbSet<patch_policy_status> patch_policy_status { get; set; }
        public virtual DbSet<performance_day_data> performance_day_data { get; set; }
        public virtual DbSet<performance_month_data> performance_month_data { get; set; }
        public virtual DbSet<performance_week_data> performance_week_data { get; set; }
        public virtual DbSet<performance_year_data> performance_year_data { get; set; }
        public virtual DbSet<policy_gfi_classification> policy_gfi_classification { get; set; }
        public virtual DbSet<policy_gfi_products> policy_gfi_products { get; set; }
        public virtual DbSet<predefined_network_check> predefined_network_check { get; set; }
        public virtual DbSet<predefined_services_check> predefined_services_check { get; set; }
        public virtual DbSet<problem_type> problem_type { get; set; }
        public virtual DbSet<processes_day_data> processes_day_data { get; set; }
        public virtual DbSet<processes_month_data> processes_month_data { get; set; }
        public virtual DbSet<processes_week_data> processes_week_data { get; set; }
        public virtual DbSet<processes_year_data> processes_year_data { get; set; }
        public virtual DbSet<project> projects { get; set; }
        public virtual DbSet<project_files> project_files { get; set; }
        public virtual DbSet<project_history> project_history { get; set; }
        public virtual DbSet<project_links> project_links { get; set; }
        public virtual DbSet<project_log> project_log { get; set; }
        public virtual DbSet<project_users> project_users { get; set; }
        public virtual DbSet<QRTZ_CALENDARS> QRTZ_CALENDARS { get; set; }
        public virtual DbSet<QRTZ_CRON_TRIGGERS> QRTZ_CRON_TRIGGERS { get; set; }
        public virtual DbSet<QRTZ_FIRED_TRIGGERS> QRTZ_FIRED_TRIGGERS { get; set; }
        public virtual DbSet<QRTZ_JOB_DETAILS> QRTZ_JOB_DETAILS { get; set; }
        public virtual DbSet<QRTZ_JOB_LISTENERS> QRTZ_JOB_LISTENERS { get; set; }
        public virtual DbSet<QRTZ_LOCKS> QRTZ_LOCKS { get; set; }
        public virtual DbSet<QRTZ_PAUSED_TRIGGER_GRPS> QRTZ_PAUSED_TRIGGER_GRPS { get; set; }
        public virtual DbSet<QRTZ_SCHEDULER_STATE> QRTZ_SCHEDULER_STATE { get; set; }
        public virtual DbSet<QRTZ_SIMPLE_TRIGGERS> QRTZ_SIMPLE_TRIGGERS { get; set; }
        public virtual DbSet<QRTZ_TRIGGER_LISTENERS> QRTZ_TRIGGER_LISTENERS { get; set; }
        public virtual DbSet<QRTZ_TRIGGERS> QRTZ_TRIGGERS { get; set; }
        public virtual DbSet<quick_list> quick_list { get; set; }
        public virtual DbSet<reminder> reminders { get; set; }
        public virtual DbSet<remote_active_sessions> remote_active_sessions { get; set; }
        public virtual DbSet<rules_emails> rules_emails { get; set; }
        public virtual DbSet<satisfaction_survey> satisfaction_survey { get; set; }
        public virtual DbSet<schedule_task> schedule_task { get; set; }
        public virtual DbSet<service_req> service_req { get; set; }
        public virtual DbSet<service_req_data> service_req_data { get; set; }
        public virtual DbSet<service_req_files> service_req_files { get; set; }
        public virtual DbSet<service_req_history> service_req_history { get; set; }
        public virtual DbSet<service_req_links> service_req_links { get; set; }
        public virtual DbSet<service_req_log> service_req_log { get; set; }
        public virtual DbSet<service_req_msg> service_req_msg { get; set; }
        public virtual DbSet<services_day_data> services_day_data { get; set; }
        public virtual DbSet<services_month_data> services_month_data { get; set; }
        public virtual DbSet<services_week_data> services_week_data { get; set; }
        public virtual DbSet<services_year_data> services_year_data { get; set; }
        public virtual DbSet<share_and_compare> share_and_compare { get; set; }
        public virtual DbSet<software> softwares { get; set; }
        public virtual DbSet<software_files> software_files { get; set; }
        public virtual DbSet<software_history> software_history { get; set; }
        public virtual DbSet<software_links> software_links { get; set; }
        public virtual DbSet<software2install_name> software2install_name { get; set; }
        public virtual DbSet<sort_cust_values> sort_cust_values { get; set; }
        public virtual DbSet<sr_sub_tab> sr_sub_tab { get; set; }
        public virtual DbSet<sr_sub_tab_files> sr_sub_tab_files { get; set; }
        public virtual DbSet<sr_sub_tab_history> sr_sub_tab_history { get; set; }
        public virtual DbSet<sr_sub_tab_links> sr_sub_tab_links { get; set; }
        public virtual DbSet<sr_sub_type> sr_sub_type { get; set; }
        public virtual DbSet<statistics_data> statistics_data { get; set; }
        public virtual DbSet<status_settings> status_settings { get; set; }
        public virtual DbSet<sub_tab_views> sub_tab_views { get; set; }
        public virtual DbSet<supplier> suppliers { get; set; }
        public virtual DbSet<supplier_files> supplier_files { get; set; }
        public virtual DbSet<supplier_history> supplier_history { get; set; }
        public virtual DbSet<supplier_links> supplier_links { get; set; }
        public virtual DbSet<survey_answers> survey_answers { get; set; }
        public virtual DbSet<survey_questions> survey_questions { get; set; }
        public virtual DbSet<sysaid_events> sysaid_events { get; set; }
        public virtual DbSet<sysaid_item_links> sysaid_item_links { get; set; }
        public virtual DbSet<sysaid_user> sysaid_user { get; set; }
        public virtual DbSet<sysaid_user_files> sysaid_user_files { get; set; }
        public virtual DbSet<sysaid_user_history> sysaid_user_history { get; set; }
        public virtual DbSet<sysaid_user_links> sysaid_user_links { get; set; }
        public virtual DbSet<sysaid_user_permissions> sysaid_user_permissions { get; set; }
        public virtual DbSet<sysaid_user_push_enable> sysaid_user_push_enable { get; set; }
        public virtual DbSet<sysaid_user_routing> sysaid_user_routing { get; set; }
        public virtual DbSet<task> tasks { get; set; }
        public virtual DbSet<task_activities> task_activities { get; set; }
        public virtual DbSet<task_files> task_files { get; set; }
        public virtual DbSet<task_history> task_history { get; set; }
        public virtual DbSet<task_links> task_links { get; set; }
        public virtual DbSet<task_log> task_log { get; set; }
        public virtual DbSet<task_users> task_users { get; set; }
        public virtual DbSet<traps_data> traps_data { get; set; }
        public virtual DbSet<ui_menus> ui_menus { get; set; }
        public virtual DbSet<ui_menus2group> ui_menus2group { get; set; }
        public virtual DbSet<url_day_data> url_day_data { get; set; }
        public virtual DbSet<url_embed_data> url_embed_data { get; set; }
        public virtual DbSet<url_month_data> url_month_data { get; set; }
        public virtual DbSet<url_week_data> url_week_data { get; set; }
        public virtual DbSet<url_year_data> url_year_data { get; set; }
        public virtual DbSet<user_answer_attempts> user_answer_attempts { get; set; }
        public virtual DbSet<user_favorites> user_favorites { get; set; }
        public virtual DbSet<user_groups> user_groups { get; set; }
        public virtual DbSet<user_questions> user_questions { get; set; }
        public virtual DbSet<user2asset> user2asset { get; set; }
        public virtual DbSet<user2ci> user2ci { get; set; }
        public virtual DbSet<user2group> user2group { get; set; }
        public virtual DbSet<users_remote_assets> users_remote_assets { get; set; }
        public virtual DbSet<uss_notif_events> uss_notif_events { get; set; }
        public virtual DbSet<uss_security_questions> uss_security_questions { get; set; }
        public virtual DbSet<work_report> work_report { get; set; }
        public virtual DbSet<computer_changes> computer_changes { get; set; }
        public virtual DbSet<computer_lists> computer_lists { get; set; }
        public virtual DbSet<custom_services> custom_services { get; set; }
        public virtual DbSet<form_history> form_history { get; set; }
        public virtual DbSet<mdm_actions> mdm_actions { get; set; }
        public virtual DbSet<priority_matrix_cust_values> priority_matrix_cust_values { get; set; }
        public virtual DbSet<QRTZ_BLOB_TRIGGERS> QRTZ_BLOB_TRIGGERS { get; set; }
        public virtual DbSet<sr_sub_tab_populate> sr_sub_tab_populate { get; set; }
        public virtual DbSet<sr_tab_dependences> sr_tab_dependences { get; set; }
        public virtual DbSet<sysaid_user_push_notifications> sysaid_user_push_notifications { get; set; }
        public virtual DbSet<version> versions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<chat_queue>()
                .Property(e => e.add_hour_in_chat_session)
                .IsFixedLength();

            modelBuilder.Entity<chat_queue>()
                .Property(e => e.allow_offline_chat)
                .IsFixedLength();

            modelBuilder.Entity<chat_queue>()
                .Property(e => e.display_details_screen)
                .IsFixedLength();

            modelBuilder.Entity<computer>()
                .Property(e => e.disable)
                .IsFixedLength();

            modelBuilder.Entity<computer>()
                .Property(e => e.manual_asset)
                .IsFixedLength();

            modelBuilder.Entity<computer>()
                .Property(e => e.patch_enabled)
                .HasPrecision(1, 0);

            modelBuilder.Entity<computer_attributes>()
                .Property(e => e.memory_physical)
                .HasPrecision(18, 0);

            modelBuilder.Entity<computer_attributes_history>()
                .Property(e => e.memory_physical)
                .HasPrecision(18, 0);

            modelBuilder.Entity<computer_history>()
                .Property(e => e.disable)
                .IsFixedLength();

            modelBuilder.Entity<computer_history>()
                .Property(e => e.manual_asset)
                .IsFixedLength();

            modelBuilder.Entity<computer_history>()
                .Property(e => e.patch_enabled)
                .HasPrecision(1, 0);

            modelBuilder.Entity<current_sla_results>()
                .Property(e => e.final_result)
                .IsFixedLength();

            modelBuilder.Entity<custom_columns>()
                .Property(e => e.compatibility_mode)
                .IsFixedLength();

            modelBuilder.Entity<custom_columns>()
                .Property(e => e.upload_from_file)
                .IsFixedLength();

            modelBuilder.Entity<custom_triggers>()
                .Property(e => e.compatibility_mode)
                .IsFixedLength();

            modelBuilder.Entity<discovery_service>()
                .Property(e => e.windows)
                .IsFixedLength();

            modelBuilder.Entity<faq>()
                .Property(e => e.kb)
                .IsFixedLength();

            modelBuilder.Entity<faq>()
                .Property(e => e.faq1)
                .IsFixedLength();

            modelBuilder.Entity<generic_messages>()
                .Property(e => e.msg_type)
                .IsFixedLength();

            modelBuilder.Entity<generic_messages>()
                .Property(e => e.disable)
                .IsFixedLength();

            modelBuilder.Entity<list_view>()
                .Property(e => e.enable_delete)
                .IsFixedLength();

            modelBuilder.Entity<mdm_policy>()
                .Property(e => e.enable_password)
                .IsFixedLength();

            modelBuilder.Entity<mdm_policy>()
                .Property(e => e.allow_simple_password)
                .IsFixedLength();

            modelBuilder.Entity<mdm_policy>()
                .Property(e => e.alphanumeric_password_req)
                .IsFixedLength();

            modelBuilder.Entity<mdm_policy>()
                .Property(e => e.use_ssl)
                .IsFixedLength();

            modelBuilder.Entity<mdm_policy>()
                .Property(e => e.outgoing_use_ssl)
                .IsFixedLength();

            modelBuilder.Entity<mdm_wifi_policy>()
                .Property(e => e.auto_join)
                .IsFixedLength();

            modelBuilder.Entity<mdm_wifi_policy>()
                .Property(e => e.hidden_network)
                .IsFixedLength();

            modelBuilder.Entity<measurements_def>()
                .Property(e => e.calculated)
                .IsFixedLength();

            modelBuilder.Entity<measurements_def>()
                .Property(e => e.enabled)
                .IsFixedLength();

            modelBuilder.Entity<measurements_def_history>()
                .Property(e => e.calculated)
                .IsFixedLength();

            modelBuilder.Entity<measurements_def_history>()
                .Property(e => e.enabled)
                .IsFixedLength();

            modelBuilder.Entity<message>()
                .Property(e => e.display_to_user)
                .IsFixedLength();

            modelBuilder.Entity<monitor_embed_data>()
                .Property(e => e.is_server)
                .IsFixedLength();

            modelBuilder.Entity<monitor_templates>()
                .Property(e => e.is_server)
                .IsFixedLength();

            modelBuilder.Entity<monitoring_conf>()
                .Property(e => e.is_server)
                .IsFixedLength();

            modelBuilder.Entity<monitoring_conf>()
                .Property(e => e.monitoring_enabled)
                .IsFixedLength();

            modelBuilder.Entity<news>()
                .Property(e => e.present)
                .IsFixedLength();

            modelBuilder.Entity<news>()
                .Property(e => e.administrator)
                .IsFixedLength();

            modelBuilder.Entity<news>()
                .Property(e => e.urgency)
                .IsFixedLength();

            modelBuilder.Entity<patch>()
                .Property(e => e.uninstallable)
                .HasPrecision(1, 0);

            modelBuilder.Entity<patch>()
                .Property(e => e.security_update)
                .HasPrecision(1, 0);

            modelBuilder.Entity<patch_policy_event>()
                .Property(e => e.is_scan)
                .HasPrecision(1, 0);

            modelBuilder.Entity<patch_policy_event>()
                .Property(e => e.is_manual)
                .HasPrecision(1, 0);

            modelBuilder.Entity<QRTZ_CALENDARS>()
                .Property(e => e.CALENDAR_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_CRON_TRIGGERS>()
                .Property(e => e.TRIGGER_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_CRON_TRIGGERS>()
                .Property(e => e.TRIGGER_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_CRON_TRIGGERS>()
                .Property(e => e.CRON_EXPRESSION)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_CRON_TRIGGERS>()
                .Property(e => e.TIME_ZONE_ID)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_FIRED_TRIGGERS>()
                .Property(e => e.ENTRY_ID)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_FIRED_TRIGGERS>()
                .Property(e => e.TRIGGER_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_FIRED_TRIGGERS>()
                .Property(e => e.TRIGGER_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_FIRED_TRIGGERS>()
                .Property(e => e.IS_VOLATILE)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_FIRED_TRIGGERS>()
                .Property(e => e.INSTANCE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_FIRED_TRIGGERS>()
                .Property(e => e.STATE)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_FIRED_TRIGGERS>()
                .Property(e => e.JOB_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_FIRED_TRIGGERS>()
                .Property(e => e.JOB_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_FIRED_TRIGGERS>()
                .Property(e => e.IS_STATEFUL)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_FIRED_TRIGGERS>()
                .Property(e => e.REQUESTS_RECOVERY)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_JOB_DETAILS>()
                .Property(e => e.JOB_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_JOB_DETAILS>()
                .Property(e => e.JOB_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_JOB_DETAILS>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_JOB_DETAILS>()
                .Property(e => e.JOB_CLASS_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_JOB_DETAILS>()
                .Property(e => e.IS_DURABLE)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_JOB_DETAILS>()
                .Property(e => e.IS_VOLATILE)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_JOB_DETAILS>()
                .Property(e => e.IS_STATEFUL)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_JOB_DETAILS>()
                .Property(e => e.REQUESTS_RECOVERY)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_JOB_DETAILS>()
                .HasMany(e => e.QRTZ_JOB_LISTENERS)
                .WithRequired(e => e.QRTZ_JOB_DETAILS)
                .HasForeignKey(e => new { e.JOB_NAME, e.JOB_GROUP });

            modelBuilder.Entity<QRTZ_JOB_DETAILS>()
                .HasMany(e => e.QRTZ_TRIGGERS)
                .WithRequired(e => e.QRTZ_JOB_DETAILS)
                .HasForeignKey(e => new { e.JOB_NAME, e.JOB_GROUP })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QRTZ_JOB_LISTENERS>()
                .Property(e => e.JOB_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_JOB_LISTENERS>()
                .Property(e => e.JOB_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_JOB_LISTENERS>()
                .Property(e => e.JOB_LISTENER)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_LOCKS>()
                .Property(e => e.LOCK_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_PAUSED_TRIGGER_GRPS>()
                .Property(e => e.TRIGGER_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_SCHEDULER_STATE>()
                .Property(e => e.INSTANCE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_SCHEDULER_STATE>()
                .Property(e => e.RECOVERER)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_SIMPLE_TRIGGERS>()
                .Property(e => e.TRIGGER_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_SIMPLE_TRIGGERS>()
                .Property(e => e.TRIGGER_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGER_LISTENERS>()
                .Property(e => e.TRIGGER_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGER_LISTENERS>()
                .Property(e => e.TRIGGER_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGER_LISTENERS>()
                .Property(e => e.TRIGGER_LISTENER)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .Property(e => e.TRIGGER_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .Property(e => e.TRIGGER_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .Property(e => e.JOB_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .Property(e => e.JOB_GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .Property(e => e.IS_VOLATILE)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .Property(e => e.TRIGGER_STATE)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .Property(e => e.TRIGGER_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .Property(e => e.CALENDAR_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .HasOptional(e => e.QRTZ_CRON_TRIGGERS)
                .WithRequired(e => e.QRTZ_TRIGGERS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .HasOptional(e => e.QRTZ_SIMPLE_TRIGGERS)
                .WithRequired(e => e.QRTZ_TRIGGERS)
                .WillCascadeOnDelete();

            modelBuilder.Entity<QRTZ_TRIGGERS>()
                .HasMany(e => e.QRTZ_TRIGGER_LISTENERS)
                .WithRequired(e => e.QRTZ_TRIGGERS)
                .HasForeignKey(e => new { e.TRIGGER_NAME, e.TRIGGER_GROUP });

            modelBuilder.Entity<quick_list>()
                .Property(e => e.quick_visible)
                .IsFixedLength();

            modelBuilder.Entity<rules_emails>()
                .Property(e => e.rule_enabled)
                .IsFixedLength();

            modelBuilder.Entity<schedule_task>()
                .Property(e => e.schedule)
                .IsFixedLength();

            modelBuilder.Entity<service_req>()
                .Property(e => e.known_error)
                .IsFixedLength();

            modelBuilder.Entity<service_req>()
                .Property(e => e.visible_to_eu)
                .IsFixedLength();

            modelBuilder.Entity<service_req_history>()
                .Property(e => e.known_error)
                .IsFixedLength();

            modelBuilder.Entity<service_req_history>()
                .Property(e => e.visible_to_eu)
                .IsFixedLength();

            modelBuilder.Entity<share_and_compare>()
                .Property(e => e.share_stat)
                .IsFixedLength();

            modelBuilder.Entity<share_and_compare>()
                .Property(e => e.more_is_green)
                .IsFixedLength();

            modelBuilder.Entity<sr_sub_tab>()
                .Property(e => e.auto_complete)
                .IsFixedLength();

            modelBuilder.Entity<sr_sub_tab>()
                .Property(e => e.policy_compliance)
                .IsFixedLength();

            modelBuilder.Entity<sr_sub_tab>()
                .Property(e => e.budgeted)
                .IsFixedLength();

            modelBuilder.Entity<sr_sub_tab>()
                .Property(e => e.approved)
                .IsFixedLength();

            modelBuilder.Entity<sr_sub_tab>()
                .Property(e => e.user_acceptance)
                .IsFixedLength();

            modelBuilder.Entity<sr_sub_tab_history>()
                .Property(e => e.auto_complete)
                .IsFixedLength();

            modelBuilder.Entity<sr_sub_tab_history>()
                .Property(e => e.policy_compliance)
                .IsFixedLength();

            modelBuilder.Entity<sr_sub_tab_history>()
                .Property(e => e.budgeted)
                .IsFixedLength();

            modelBuilder.Entity<sr_sub_tab_history>()
                .Property(e => e.approved)
                .IsFixedLength();

            modelBuilder.Entity<sr_sub_tab_history>()
                .Property(e => e.user_acceptance)
                .IsFixedLength();

            modelBuilder.Entity<survey_questions>()
                .Property(e => e.enabled)
                .IsFixedLength();

            modelBuilder.Entity<survey_questions>()
                .Property(e => e.display_comment)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_item_links>()
                .Property(e => e.account_id)
                .IsUnicode(false);

            modelBuilder.Entity<sysaid_user>()
                .Property(e => e.main_user)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user>()
                .Property(e => e.administrator)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user>()
                .Property(e => e.manager)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user>()
                .Property(e => e.disable)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user>()
                .Property(e => e.email_notifications)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user>()
                .Property(e => e.permissions_by_groups)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user>()
                .Property(e => e.enable_login_to_eup)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user_history>()
                .Property(e => e.main_user)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user_history>()
                .Property(e => e.administrator)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user_history>()
                .Property(e => e.manager)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user_history>()
                .Property(e => e.disable)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user_history>()
                .Property(e => e.email_notifications)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user_history>()
                .Property(e => e.permissions_by_groups)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user_history>()
                .Property(e => e.enable_login_to_eup)
                .IsFixedLength();

            modelBuilder.Entity<sysaid_user_history>()
                .Property(e => e.sr_email_notif_condition)
                .IsUnicode(false);

            modelBuilder.Entity<sysaid_user_push_enable>()
                .Property(e => e.is_production)
                .IsFixedLength();

            modelBuilder.Entity<ui_menus>()
                .Property(e => e.mi_enabled)
                .IsFixedLength();

            modelBuilder.Entity<user_groups>()
                .Property(e => e.display_group)
                .IsFixedLength();

            modelBuilder.Entity<QRTZ_BLOB_TRIGGERS>()
                .Property(e => e.TRIGGER_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<QRTZ_BLOB_TRIGGERS>()
                .Property(e => e.TRIGGER_GROUP)
                .IsUnicode(false);
        }
    }
}
