using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Devart.Data.Oracle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;




namespace USERFORM.Models
{
    public partial class ModelContext : DbContext
    {
        private string conn;

        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AtrmsDocumentsDtlMain> AtrmsDocumentsDtlMain { get; set; }
        public virtual DbSet<AtrmsDocumentsDtlTemp> AtrmsDocumentsDtlTemp { get; set; }
        public virtual DbSet<AtrmsEmailLog> AtrmsEmailLog { get; set; }
        public virtual DbSet<AtrmsExperienceDtl> AtrmsExperienceDtl { get; set; }
        public virtual DbSet<AtrmsPersonalDtl> AtrmsPersonalDtl { get; set; }
        public virtual DbSet<AtrmsQualificationDtl> AtrmsQualificationDtl { get; set; }
        public virtual DbSet<RecCategoryMsts> RecCategoryMsts { get; set; }
        public virtual DbSet<RecCodeGenerationMsts> RecCodeGenerationMsts { get; set; }
        public virtual DbSet<RecDistrictMsts> RecDistrictMsts { get; set; }
        public virtual DbSet<RecEventStatusMsts> RecEventStatusMsts { get; set; }
        public virtual DbSet<RecInstituteMsts> RecInstituteMsts { get; set; }
        public virtual DbSet<RecPostAvailableMsts> RecPostAvailableMsts { get; set; }
        public virtual DbSet<RecPostMsts> RecPostMsts { get; set; }
        public virtual DbSet<RecPostStatusMsts> RecPostStatusMsts { get; set; }
        public virtual DbSet<RecQualificationMsts> RecQualificationMsts { get; set; }
        public virtual DbSet<RecStateMsts> RecStateMsts { get; set; }
        public virtual DbSet<RecUniversityMsts> RecUniversityMsts { get; set; }


        public int insertUpdateToDB(string sql)
        {
            OracleConnection connection = new OracleConnection(conn);
            OracleCommand cmd = new OracleCommand();
            int i = 0;
            try
            {
                cmd.CommandText = sql.ToString();
                cmd.Connection = connection;
                connection.Open();
                i = cmd.ExecuteNonQuery();
                connection.Close();
                return i;
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                return i = 0;
            }
        }

        public int GetScalerFromDB(string sql)
        {
            OracleConnection connection = new OracleConnection(conn);
            OracleCommand cmd = new OracleCommand();
            int i = 0;
            try
            {
                cmd.CommandText = sql.ToString();
                cmd.Connection = connection;
                connection.Open();
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                return i = 0;
            }
        }

        public string GetCharScalerFromDB(string sql)
        {
            OracleConnection connection = new OracleConnection(conn);
            OracleCommand cmd = new OracleCommand();
            int i = 0;
            try
            {
                cmd.CommandText = sql.ToString();
                cmd.Connection = connection;
                connection.Open();
                string result = Convert.ToString(cmd.ExecuteScalar());
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                return null;
            }
        }

        public int ExecuteProcedure(string procedure, params object[] parameters)
        {
            List<OracleParameter> oracleParameterList = ((List<OracleParameter>)parameters[0]);
            string[] oracleParameters = new string[oracleParameterList.Count];
            string query = "BEGIN " + procedure + "(";
            for (int i = 0; i < oracleParameterList.Count; i++)
            {
                OracleParameter parameter = oracleParameterList[i] as OracleParameter;
                oracleParameters[i] = ":" + parameter.ParameterName;
            }
            query += string.Join(",", oracleParameters);
            query += "); end;";
            //Database.OpenConnection()
            return Database.ExecuteSqlCommand(query, oracleParameterList);
        }



        public int ExecuteProcedureForRefCursor(string procedure, params object[] parameters)
        {
            List<OracleParameter> oracleParameterList = ((List<OracleParameter>)parameters[0]);
            string[] oracleParameters = new string[oracleParameterList.Count];
            string query = "BEGIN " + procedure + "(";
            for (int i = 0; i < oracleParameterList.Count; i++)
            {
                OracleParameter parameter = oracleParameterList[i] as OracleParameter;
                oracleParameters[i] = ":" + parameter.ParameterName;
            }
            query += string.Join(",", oracleParameters);
            query += "); end;";


            Database.OpenConnection();
            int x = Database.ExecuteSqlCommand(query, oracleParameterList);
            return x;
        }

        public Task<int> ExecuteProcedureAsync<TElement>(string procedure, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteQuery<TElement>(string query)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteQueryAsync<TElement>(string query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> ExecuteSelectProcedure<TElement>(string procedure, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> ExecuteSelectQuery<TElement>(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync(CancellationToken.None);
        }
        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        //{
        //    //SyncObjectsStatePreCommit();
        //    var changesAsync = await base.SaveChangesAsync(cancellationToken);
        //    SyncObjectsStatePostCommit();
        //    return changesAsync;
        //}

        //public void SyncObjectsStatePostCommit()
        //{
        //    foreach (var dbEntityEntry in ChangeTracker.Entries())
        //    {
        //        dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
        //    }
        //}
        //private void SyncObjectsStatePreCommit()
        //{
        //    foreach (var dbEntityEntry in ChangeTracker.Entries())
        //    {
        //        dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
        //    }
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseOracle("SERVICE NAME=ORACLE;Direct=true; License Key=vEyr8GdnIarOKlEHQKxi+4E0HlXN85PVGHI096M18fHO05syciZT/8xvOeNbTwuMbqdZkRZ1qbdPjO13mrnBnlMUyskKr9qbBNMTzJAp5+R858T7YUZaTY5rodcDl7pDutJBeuYiwHG+xtXnywKMPX+9u82fR1AMT9EailpEiBp1OAn6IbJ55eXY15+rsAfDDwUuIv/js610S6cy9vLt37IL4PcZ8Wx/MrQlA38Z+kEH9Wztcv+NSWFVRz2wnVRDtIowaySSKk30sA+MBbg2IIUI+/MgDUp6w53NCxQSsuM=; User Id=RECAN;Password=recan_dev; Data Source= iffcoexadr-92rdq-scan.drhyddbcltsn01.drhydebsprodvcn.oraclevcn.com:1521/ifppdbdev.drhyddbcltsn01.drhydebsprodvcn.oraclevcn.com;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AtrmsDocumentsDtlMain>(entity =>
            {
                entity.HasKey(e => new { e.Sno, e.AtId });

                entity.ToTable("ATRMS_DOCUMENTS_DTL_MAIN", "RECAN");

                entity.HasIndex(e => new { e.Sno, e.AtId })
                    .HasName("SYS_C0044024")
                    .IsUnique();

                entity.Property(e => e.Sno).HasColumnName("SNO");

                entity.Property(e => e.AtId)
                    .HasColumnName("AT_ID")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);

                entity.Property(e => e.FileName)
                    .HasColumnName("FILE_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(2000);

                entity.Property(e => e.Fileid).HasColumnName("FILEID");

                entity.Property(e => e.Filesize).HasColumnName("FILESIZE");

                entity.Property(e => e.Mimetype)
                    .HasColumnName("MIMETYPE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(200);

                entity.Property(e => e.RecCode)
                    .HasColumnName("REC_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.StatusDt)
                    .HasColumnName("STATUS_DT")
                    .HasColumnType("date");

                entity.Property(e => e.Uploadfile).HasColumnName("UPLOADFILE");
            });

            modelBuilder.Entity<AtrmsDocumentsDtlTemp>(entity =>
            {
                entity.HasKey(e => new { e.Sno, e.AtId });

                entity.ToTable("ATRMS_DOCUMENTS_DTL_TEMP", "RECAN");

                entity.HasIndex(e => new { e.Sno, e.AtId })
                    .HasName("SYS_C0044025")
                    .IsUnique();

                entity.Property(e => e.Sno).HasColumnName("SNO");

                entity.Property(e => e.AtId)
                    .HasColumnName("AT_ID")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);

                entity.Property(e => e.FileName)
                    .HasColumnName("FILE_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(2000);

                entity.Property(e => e.Fileid).HasColumnName("FILEID");

                entity.Property(e => e.Filesize).HasColumnName("FILESIZE");

                entity.Property(e => e.Mimetype)
                    .HasColumnName("MIMETYPE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(200);

                entity.Property(e => e.RecCode)
                    .HasColumnName("REC_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.StatusDt)
                    .HasColumnName("STATUS_DT")
                    .HasColumnType("date");

                entity.Property(e => e.Uploadfile).HasColumnName("UPLOADFILE");
            });

            modelBuilder.Entity<AtrmsEmailLog>(entity =>
            {
                entity.HasKey(e => e.AtId);

                entity.ToTable("ATRMS_EMAIL_LOG", "RECAN");

                entity.HasIndex(e => e.AtId)
                    .HasName("SYS_C0043703")
                    .IsUnique();

                entity.Property(e => e.AtId)
                    .HasColumnName("AT_ID")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);

                entity.Property(e => e.CcEmail)
                    .HasColumnName("CC_EMAIL")
                    .HasColumnType("varchar2")
                    .HasMaxLength(200);

                entity.Property(e => e.CcEmail2)
                    .HasColumnName("CC_EMAIL2")
                    .HasColumnType("varchar2")
                    .HasMaxLength(200);

                entity.Property(e => e.CcEmail3)
                    .HasColumnName("CC_EMAIL3")
                    .HasColumnType("varchar2")
                    .HasMaxLength(200);

                entity.Property(e => e.EmailAttempt)
                    .HasColumnName("EMAIL_ATTEMPT")
                    .HasColumnType("varchar2")
                    .HasMaxLength(2);

                entity.Property(e => e.EmailStatus)
                    .HasColumnName("EMAIL_STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.ErrorMsg)
                    .HasColumnName("ERROR_MSG")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.FromAddress)
                    .HasColumnName("FROM_ADDRESS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(200);

                entity.Property(e => e.MailBody)
                    .HasColumnName("MAIL_BODY")
                    .HasColumnType("varchar2")
                    .HasMaxLength(4000);

                entity.Property(e => e.MailHtml)
                    .HasColumnName("MAIL_HTML")
                    .HasColumnType("clob");

                entity.Property(e => e.MailSubj)
                    .HasColumnName("MAIL_SUBJ")
                    .HasColumnType("varchar2")
                    .HasMaxLength(80);

                entity.Property(e => e.Module)
                    .HasColumnName("MODULE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(15);

                entity.Property(e => e.Projectid)
                    .HasColumnName("PROJECTID")
                    .HasColumnType("varchar2")
                    .HasMaxLength(5);

                entity.Property(e => e.RecCode)
                    .HasColumnName("REC_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.RecordDt)
                    .HasColumnName("RECORD_DT")
                    .HasColumnType("date");

                entity.Property(e => e.RequestDatetime)
                    .HasColumnName("REQUEST_DATETIME")
                    .HasColumnType("date");

                entity.Property(e => e.RollNo).HasColumnName("ROLL_NO");

                entity.Property(e => e.SendingDatetime)
                    .HasColumnName("SENDING_DATETIME")
                    .HasColumnType("date");

                entity.Property(e => e.SeqNo).HasColumnName("SEQ_NO");

                entity.Property(e => e.ToEmail)
                    .HasColumnName("TO_EMAIL")
                    .HasColumnType("varchar2")
                    .HasMaxLength(200);

                entity.Property(e => e.ToEmail2)
                    .HasColumnName("TO_EMAIL2")
                    .HasColumnType("varchar2")
                    .HasMaxLength(200);

                entity.Property(e => e.ToEmail3)
                    .HasColumnName("TO_EMAIL3")
                    .HasColumnType("varchar2")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<AtrmsExperienceDtl>(entity =>
            {
                entity.HasKey(e => new { e.Sno, e.AtId });

                entity.ToTable("ATRMS_EXPERIENCE_DTL", "RECAN");

                entity.HasIndex(e => new { e.Sno, e.AtId })
                    .HasName("SYS_C0044023")
                    .IsUnique();

                entity.Property(e => e.Sno).HasColumnName("SNO");

                entity.Property(e => e.AtId)
                    .HasColumnName("AT_ID")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("CREATED_ON")
                    .HasColumnType("date");

                entity.Property(e => e.JobDescription)
                    .HasColumnName("JOB_DESCRIPTION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.JobPosition)
                    .HasColumnName("JOB_POSITION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(200);

                entity.Property(e => e.LastSalaryDrawn)
                    .HasColumnName("LAST_SALARY_DRAWN")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.NameOfOrganization)
                    .HasColumnName("NAME_OF_ORGANIZATION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(200);

                entity.Property(e => e.PeriodFrom)
                    .HasColumnName("PERIOD_FROM")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.PeriodTo)
                    .HasColumnName("PERIOD_TO")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.RecCode)
                    .HasColumnName("REC_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.TotalExperience)
                    .HasColumnName("TOTAL_EXPERIENCE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AtrmsPersonalDtl>(entity =>
            {
                entity.HasKey(e => e.AtId);

                entity.ToTable("ATRMS_PERSONAL_DTL", "RECAN");

                entity.HasIndex(e => e.AtId)
                    .HasName("SYS_C0043814")
                    .IsUnique();

                entity.Property(e => e.AtId)
                    .HasColumnName("AT_ID")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);

                entity.Property(e => e.AadharNo).HasColumnName("AADHAR_NO");

                entity.Property(e => e.AcceptedBy).HasColumnName("ACCEPTED_BY");

                entity.Property(e => e.AcceptedOn)
                    .HasColumnName("ACCEPTED_ON")
                    .HasColumnType("date");

                entity.Property(e => e.AlternateEmailId)
                    .HasColumnName("ALTERNATE_EMAIL_ID")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.AlternateNumber).HasColumnName("ALTERNATE_NUMBER");

                entity.Property(e => e.Area)
                    .HasColumnName("AREA")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.BirthPlace)
                    .HasColumnName("BIRTH_PLACE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.CAddress)
                    .HasColumnName("C_ADDRESS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(499);

                entity.Property(e => e.Category)
                    .HasColumnName("CATEGORY")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.City)
                    .HasColumnName("CITY")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .HasColumnName("COUNTRY")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("DATE_OF_BIRTH")
                    .HasColumnType("date");

                entity.Property(e => e.District)
                    .HasColumnName("DISTRICT")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.EmailId)
                    .HasColumnName("EMAIL_ID")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.ExApperentice)
                    .HasColumnName("EX_APPERENTICE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.FatherName)
                    .HasColumnName("FATHER_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.FatherOccupation)
                    .HasColumnName("FATHER_OCCUPATION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.FirstName)
                    .HasColumnName("FIRST_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .HasColumnName("GENDER")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.Hometown)
                    .HasColumnName("HOMETOWN")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.HouseNo)
                    .HasColumnName("HOUSE_NO")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.IdentificationMark)
                    .HasColumnName("IDENTIFICATION_MARK")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.IffcoEmployee)
                    .HasColumnName("IFFCO_EMPLOYEE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(3);

                entity.Property(e => e.LandLoser)
                    .HasColumnName("LAND_LOSER")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .HasColumnName("LAST_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.MaritalStatus)
                    .HasColumnName("MARITAL_STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.MhrdNats)
                    .HasColumnName("MHRD_NATS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("MIDDLE_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.MobileNumber).HasColumnName("MOBILE_NUMBER");

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.MotherName)
                    .HasColumnName("MOTHER_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.MotherOccupation)
                    .HasColumnName("MOTHER_OCCUPATION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.NameEmpExchange)
                    .HasColumnName("NAME_EMP_EXCHANGE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.Nationality)
                    .HasColumnName("NATIONALITY")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);

                entity.Property(e => e.PAddress)
                    .HasColumnName("P_ADDRESS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(500);

                entity.Property(e => e.Pincode).HasColumnName("PINCODE");

                entity.Property(e => e.PostAppliedCode)
                    .HasColumnName("POST_APPLIED_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.PostAppliedDescription)
                    .HasColumnName("POST_APPLIED_DESCRIPTION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.Qualification)
                    .HasColumnName("QUALIFICATION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.RecCode)
                    .HasColumnName("REC_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.RecEventCode)
                    .HasColumnName("REC_EVENT_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.RecEventStatus)
                    .HasColumnName("REC_EVENT_STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.RegNoEmpExchange)
                    .HasColumnName("REG_NO_EMP_EXCHANGE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.RegNumberMhrdNats)
                    .HasColumnName("REG_NUMBER_MHRD_NATS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.RejectedBy).HasColumnName("REJECTED_BY");

                entity.Property(e => e.RejectedOn)
                    .HasColumnName("REJECTED_ON")
                    .HasColumnType("date");

                entity.Property(e => e.RelativeEmpId).HasColumnName("RELATIVE_EMP_ID");

                entity.Property(e => e.RelativeName)
                    .HasColumnName("RELATIVE_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.RelativePost)
                    .HasColumnName("RELATIVE_POST")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.RelativeRelation)
                    .HasColumnName("RELATIVE_RELATION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.RelativeUnit)
                    .HasColumnName("RELATIVE_UNIT")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);

                entity.Property(e => e.Religion)
                    .HasColumnName("RELIGION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);

                entity.Property(e => e.Remarks)
                    .HasColumnName("REMARKS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(500);

                entity.Property(e => e.RollNo).HasColumnName("ROLL_NO");

                entity.Property(e => e.State)
                    .HasColumnName("STATE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.Street)
                    .HasColumnName("STREET")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.SubmitDate)
                    .HasColumnName("SUBMIT_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.TotExp)
                    .HasColumnName("TOT_EXP")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.UnitCode).HasColumnName("UNIT_CODE");
            });

            modelBuilder.Entity<AtrmsQualificationDtl>(entity =>
            {
                entity.HasKey(e => new { e.Sno, e.AtId });

                entity.ToTable("ATRMS_QUALIFICATION_DTL", "RECAN");

                entity.HasIndex(e => new { e.Sno, e.AtId })
                    .HasName("SYS_C0044022")
                    .IsUnique();

                entity.Property(e => e.Sno).HasColumnName("SNO");

                entity.Property(e => e.AtId)
                    .HasColumnName("AT_ID")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("CREATED_ON")
                    .HasColumnType("date");

                entity.Property(e => e.MarksObtained)
                    .HasColumnName("MARKS_OBTAINED")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.NameOfBoard)
                    .HasColumnName("NAME_OF_BOARD")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.Percentage)
                    .HasColumnName("PERCENTAGE")
                    .HasColumnType("double");

                entity.Property(e => e.Qualification)
                    .HasColumnName("QUALIFICATION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.RecCode)
                    .HasColumnName("REC_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.Subject)
                    .HasColumnName("SUBJECT")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.TotalMarks)
                    .HasColumnName("TOTAL_MARKS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.UnitCode).HasColumnName("UNIT_CODE");

                entity.Property(e => e.YearOfPassing)
                    .HasColumnName("YEAR_OF_PASSING")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RecCategoryMsts>(entity =>
            {
                entity.HasKey(e => new { e.RecCode, e.CategoryCode });

                entity.ToTable("REC_CATEGORY_MSTS", "RECAN");

                entity.HasIndex(e => new { e.RecCode, e.CategoryCode })
                    .HasName("PK_REC_CATEGORY_MSTS")
                    .IsUnique();

                entity.Property(e => e.RecCode)
                    .HasColumnName("REC_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.CategoryCode)
                    .HasColumnName("CATEGORY_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.Category)
                    .HasColumnName("CATEGORY")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("CREATED_DATETIME")
                    .HasColumnType("date");

                entity.Property(e => e.Deceased)
                    .HasColumnName("DECEASED")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.ExApp)
                    .HasColumnName("EX_APP")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.Landloser)
                    .HasColumnName("LANDLOSER")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.MaxAge).HasColumnName("MAX_AGE");

                entity.Property(e => e.MaxDate)
                    .HasColumnName("MAX_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.MinAge).HasColumnName("MIN_AGE");

                entity.Property(e => e.MinDate)
                    .HasColumnName("MIN_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.MinPassedYear).HasColumnName("MIN_PASSED_YEAR");

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.OnDate)
                    .HasColumnName("ON_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.QualifyingMarksGeneralObc).HasColumnName("QUALIFYING_MARKS_GENERAL_OBC");

                entity.Property(e => e.QualifyingMarksScSt).HasColumnName("QUALIFYING_MARKS_SC_ST");

                entity.Property(e => e.UnitCode).HasColumnName("UNIT_CODE");
            });

            modelBuilder.Entity<RecCodeGenerationMsts>(entity =>
            {
                entity.HasKey(e => e.RecCode);

                entity.ToTable("REC_CODE_GENERATION_MSTS", "RECAN");

                entity.HasIndex(e => e.RecCode)
                    .HasName("SYS_C0043178")
                    .IsUnique();

                entity.Property(e => e.RecCode)
                    .HasColumnName("REC_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("CREATED_DATETIME")
                    .HasColumnType("date");

                entity.Property(e => e.DateOfTest)
                    .HasColumnName("DATE_OF_TEST")
                    .HasColumnType("date");

                entity.Property(e => e.FyYear)
                    .HasColumnName("FY_YEAR")
                    .HasColumnType("varchar2")
                    .HasMaxLength(15);

                entity.Property(e => e.LastDate)
                    .HasColumnName("LAST_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.NotifyDtRecruitment)
                    .HasColumnName("NOTIFY_DT_RECRUITMENT")
                    .HasColumnType("date");

                entity.Property(e => e.RecStatus)
                    .HasColumnName("REC_STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.Remarks)
                    .HasColumnName("REMARKS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.UnitCode).HasColumnName("UNIT_CODE");
            });

            modelBuilder.Entity<RecDistrictMsts>(entity =>
            {
                entity.HasKey(e => e.DisttCd);

                entity.ToTable("REC_DISTRICT_MSTS", "RECAN");

                entity.HasIndex(e => e.DisttCd)
                    .HasName("PK_REC_DISTRICT_MSTS")
                    .IsUnique();

                entity.Property(e => e.DisttCd)
                    .HasColumnName("DISTT_CD")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("CREATION_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.DisttName)
                    .HasColumnName("DISTT_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(40);

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.StateCd)
                    .HasColumnName("STATE_CD")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.HasOne(d => d.StateCdNavigation)
                    .WithMany(p => p.RecDistrictMsts)
                    .HasForeignKey(d => d.StateCd)
                    .HasConstraintName("FK_REC_DISTRICT_STATE");
            });

            modelBuilder.Entity<RecEventStatusMsts>(entity =>
            {
                entity.HasKey(e => e.RecEventCode);

                entity.ToTable("REC_EVENT_STATUS_MSTS", "RECAN");

                entity.HasIndex(e => e.RecEventCode)
                    .HasName("PK_REC_EVENT_STATUS_MSTS")
                    .IsUnique();

                entity.Property(e => e.RecEventCode)
                    .HasColumnName("REC_EVENT_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("CREATED_DATETIME")
                    .HasColumnType("date");

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.RecEventStatus)
                    .HasColumnName("REC_EVENT_STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<RecInstituteMsts>(entity =>
            {
                entity.HasKey(e => e.InstituteCd);

                entity.ToTable("REC_INSTITUTE_MSTS", "RECAN");

                entity.HasIndex(e => e.InstituteCd)
                    .HasName("SYS_C0042853")
                    .IsUnique();

                entity.Property(e => e.InstituteCd).HasColumnName("INSTITUTE_CD");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("CREATED_DATETIME")
                    .HasColumnType("date");

                entity.Property(e => e.DisttCd)
                    .HasColumnName("DISTT_CD")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.InstituteName)
                    .HasColumnName("INSTITUTE_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(500);

                entity.Property(e => e.InstituteType)
                    .HasColumnName("INSTITUTE_TYPE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.StateCd)
                    .HasColumnName("STATE_CD")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.UniversityId)
                    .HasColumnName("UNIVERSITY_ID")
                    .HasColumnType("varchar2")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<RecPostAvailableMsts>(entity =>
            {
                entity.HasKey(e => new { e.RecCode, e.PostAppliedCode });

                entity.ToTable("REC_POST_AVAILABLE_MSTS", "RECAN");

                entity.HasIndex(e => new { e.RecCode, e.PostAppliedCode })
                    .HasName("PK_REC_POST_AVAILABLE_MSTS")
                    .IsUnique();

                entity.Property(e => e.RecCode)
                    .HasColumnName("REC_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.PostAppliedCode)
                    .HasColumnName("POST_APPLIED_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("CREATED_DATETIME")
                    .HasColumnType("date");

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.PostAppliedDescription)
                    .HasColumnName("POST_APPLIED_DESCRIPTION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.QualCode).HasColumnName("QUAL_CODE");

                entity.Property(e => e.QualCode1).HasColumnName("QUAL_CODE_1");

                entity.Property(e => e.QualCode2).HasColumnName("QUAL_CODE_2");

                entity.Property(e => e.QualDesc)
                    .IsRequired()
                    .HasColumnName("QUAL_DESC")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.QualDesc1)
                    .HasColumnName("QUAL_DESC_1")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.QualDesc2)
                    .HasColumnName("QUAL_DESC_2")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.QualExp1)
                    .HasColumnName("QUAL_EXP_1")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.QualExp2)
                    .HasColumnName("QUAL_EXP_2")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.RecStatus)
                    .HasColumnName("REC_STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.Remarks)
                    .HasColumnName("REMARKS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.UnitCode).HasColumnName("UNIT_CODE");
            });

            modelBuilder.Entity<RecPostMsts>(entity =>
            {
                entity.HasKey(e => e.PostAppliedCode);

                entity.ToTable("REC_POST_MSTS", "RECAN");

                entity.HasIndex(e => e.PostAppliedCode)
                    .HasName("PK_REC_POST_MSTS")
                    .IsUnique();

                entity.Property(e => e.PostAppliedCode)
                    .HasColumnName("POST_APPLIED_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.AppTrade)
                    .HasColumnName("APP_TRADE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("CREATED_DATETIME")
                    .HasColumnType("date");

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.PostAppliedDescription)
                    .IsRequired()
                    .HasColumnName("POST_APPLIED_DESCRIPTION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.Remarks)
                    .HasColumnName("REMARKS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.UnitCode).HasColumnName("UNIT_CODE");
            });

            modelBuilder.Entity<RecPostStatusMsts>(entity =>
            {
                entity.HasKey(e => e.RecCode);

                entity.ToTable("REC_POST_STATUS_MSTS", "RECAN");

                entity.HasIndex(e => e.RecCode)
                    .HasName("PK_REC_STATUS_MSTS")
                    .IsUnique();

                entity.Property(e => e.RecCode)
                    .HasColumnName("REC_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("CREATED_DATETIME")
                    .HasColumnType("date");

                entity.Property(e => e.DateOfInterview)
                    .HasColumnName("DATE_OF_INTERVIEW")
                    .HasColumnType("date");

                entity.Property(e => e.DateOfTest)
                    .HasColumnName("DATE_OF_TEST")
                    .HasColumnType("date");

                entity.Property(e => e.FyYear)
                    .HasColumnName("FY_YEAR")
                    .HasColumnType("varchar2")
                    .HasMaxLength(15);

                entity.Property(e => e.LastDate)
                    .HasColumnName("LAST_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.NoOfVacancies).HasColumnName("NO_OF_VACANCIES");

                entity.Property(e => e.NotifyDtRecruitment)
                    .HasColumnName("NOTIFY_DT_RECRUITMENT")
                    .HasColumnType("date");

                entity.Property(e => e.PostAppliedDescription)
                    .IsRequired()
                    .HasColumnName("POST_APPLIED_DESCRIPTION")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.QualCode).HasColumnName("QUAL_CODE");

                entity.Property(e => e.QualCode1).HasColumnName("QUAL_CODE_1");

                entity.Property(e => e.QualCode2).HasColumnName("QUAL_CODE_2");

                entity.Property(e => e.QualDesc)
                    .IsRequired()
                    .HasColumnName("QUAL_DESC")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.QualDesc1)
                    .HasColumnName("QUAL_DESC_1")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.QualDesc2)
                    .HasColumnName("QUAL_DESC_2")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.QualExp1)
                    .HasColumnName("QUAL_EXP_1")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.QualExp2)
                    .HasColumnName("QUAL_EXP_2")
                    .HasColumnType("varchar2")
                    .HasMaxLength(50);

                entity.Property(e => e.RecEventCode)
                    .IsRequired()
                    .HasColumnName("REC_EVENT_CODE")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.RecStatus)
                    .HasColumnName("REC_STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(20);

                entity.Property(e => e.Remarks)
                    .HasColumnName("REMARKS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.UnitCode).HasColumnName("UNIT_CODE");
            });

            modelBuilder.Entity<RecQualificationMsts>(entity =>
            {
                entity.HasKey(e => e.QualCode);

                entity.ToTable("REC_QUALIFICATION_MSTS", "RECAN");

                entity.HasIndex(e => e.QualCode)
                    .HasName("PK_REC_QUALIFICATION_MSTS")
                    .IsUnique();

                entity.Property(e => e.QualCode).HasColumnName("QUAL_CODE");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedDatetime)
                    .HasColumnName("CREATED_DATETIME")
                    .HasColumnType("date");

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.QualDesc)
                    .HasColumnName("QUAL_DESC")
                    .HasColumnType("varchar2")
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<RecStateMsts>(entity =>
            {
                entity.HasKey(e => e.StateCd);

                entity.ToTable("REC_STATE_MSTS", "RECAN");

                entity.HasIndex(e => e.StateCd)
                    .HasName("PK_REC_STATE_MSTS")
                    .IsUnique();

                entity.Property(e => e.StateCd)
                    .HasColumnName("STATE_CD")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("CREATION_DATE")
                    .HasColumnType("date");

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.StateName)
                    .HasColumnName("STATE_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(40);

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<RecUniversityMsts>(entity =>
            {
                entity.HasKey(e => e.UniversityId);

                entity.ToTable("REC_UNIVERSITY_MSTS", "RECAN");

                entity.HasIndex(e => e.UniversityId)
                    .HasName("PK_REC_UNIVERSITY_MSTS")
                    .IsUnique();

                entity.Property(e => e.UniversityId).HasColumnName("UNIVERSITY_ID");

                entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");

                entity.Property(e => e.CreationDatetime)
                    .HasColumnName("CREATION_DATETIME")
                    .HasColumnType("date");

                entity.Property(e => e.DisttCd)
                    .HasColumnName("DISTT_CD")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.ModificationDt)
                    .HasColumnName("MODIFICATION_DT")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("MODIFIED_BY");

                entity.Property(e => e.StateCd)
                    .HasColumnName("STATE_CD")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.Status)
                    .HasColumnName("STATUS")
                    .HasColumnType("varchar2")
                    .HasMaxLength(10);

                entity.Property(e => e.UniversityName)
                    .HasColumnName("UNIVERSITY_NAME")
                    .HasColumnType("varchar2")
                    .HasMaxLength(500);

                entity.HasOne(d => d.DisttCdNavigation)
                    .WithMany(p => p.RecUniversityMsts)
                    .HasForeignKey(d => d.DisttCd)
                    .HasConstraintName("FK_REC_UNIVERSITY_DISTRICT");

                entity.HasOne(d => d.StateCdNavigation)
                    .WithMany(p => p.RecUniversityMsts)
                    .HasForeignKey(d => d.StateCd)
                    .HasConstraintName("FK_REC_UNIVERSITY_STATE");
            });
        }

        internal DataTable GetSQLQuery(string sqlquery)
        {
            throw new NotImplementedException();
        }
    }
}
