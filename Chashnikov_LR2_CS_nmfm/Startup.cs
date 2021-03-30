using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Chashnikov_LR2_CS.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


namespace Chashnikov_LR2_CS
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
           // string con = "Server=(localdb)\\mssqllocaldb;Database=BoyMandbstore;Trusted_Connection=True;";
            string cons = "Server=(localdb)\\mssqllocaldb;Database=SomeTastydb;Trusted_Connection=True;";
            // ������������� �������� ������
            //services.AddDbContext<ApplicationsContext>(options => options.UseSqlServer(con));
            //services.AddDbContext<DevelopersContext>(options => options.UseSqlServer(con));
            services.AddDbContext<MySystemContext>(options => options.UseSqlServer(cons));
            // services.AddDbContext<UserAndAppContext>(options => options.UseSqlServer(cons));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(options =>
                     {
                         options.RequireHttpsMetadata = false;
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                            // ��������, ����� �� �������������� �������� ��� ��������� ������
                            ValidateIssuer = true,
                            // ������, �������������� ��������
                            ValidIssuer = AuthOptions.ISSUER,

                            // ����� �� �������������� ����������� ������
                            ValidateAudience = true,
                            // ��������� ����������� ������
                            ValidAudience = AuthOptions.AUDIENCE,
                            // ����� �� �������������� ����� �������������
                            ValidateLifetime = true,

                            // ��������� ����� ������������
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // ��������� ����� ������������
                            ValidateIssuerSigningKey = true,
                         };
                     });
            services.AddControllers(); // ���������� ����������� ��� �������������






        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();  //��������� ���� ������� �� ������ ���������� �� ������ � ���-�������� �� ����
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // ���������� ������������� �� �����������
            });
        }
    }
}