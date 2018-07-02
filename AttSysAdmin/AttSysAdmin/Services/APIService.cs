using AttSysAdmin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AttSysAdmin.Services
{
    public class APIService
    {
        public async Task<string> Login(string Username, string Password)
        {           
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                            new KeyValuePair<string, string>("username",Username),
                            new KeyValuePair<string, string>("password", Password)
                        });

                var httpClient = new HttpClient();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                         System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true; // **** Always accept
                };

                var response = await httpClient.PostAsync("https://teenoh.webfactional.com/login/", formContent);
                if (response.IsSuccessStatusCode)
                {

                    HttpContent content = response.Content;
                    var json = await content.ReadAsStringAsync();
                    var TokenObject = JsonConvert.DeserializeObject<JObject>(json);

                    var token = ((App)Application.Current).token = TokenObject.Value<string>("token");
                    httpClient.Dispose();

                    return token;
                }
                else
                {
                    return null;
                }

            }


            catch (Exception e)
            {
                return null;
            }

        }


        public async Task<bool> FetchTeacherData(string token)
        {
            var tokenField = "Token " + token;
            try
            {
                var httpClient = new HttpClient();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                        delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                        {
                            return true; // **** Always accept
                        };

                httpClient.DefaultRequestHeaders.Add("Authorization", tokenField);
                var response = await httpClient.GetAsync("https://teenoh.webfactional.com/teacher/");

                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    var json = await content.ReadAsStringAsync();
                    var teacherData = JsonConvert.DeserializeObject<TeacherData>(json);
                    App.TeacherData = teacherData;
                    App.OngoingAttendances = new List<Models.OngoingAttendance>();
                    foreach (var course in teacherData.courses)
                    {
                        foreach (var attendance in course.attendances)
                        {
                            if (attendance.is_active == true)
                            {
                                var ongoingAttendance = new OngoingAttendance
                                {
                                    id = attendance.id,
                                    course_name = course.name,
                                    code = course.code,
                                    credits = course.credits,
                                    venue = attendance.venue,
                                    name = attendance.name,
                                    is_active = attendance.is_active,
                                    present_students = attendance.present_students
                                };
                                App.OngoingAttendances.Add(ongoingAttendance);
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
            }

        }


        public async Task<string> StartAttendance(string token, string name, string course, string venue)
        {
            var tokenField = "Token " + token;
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                            new KeyValuePair<string, string>("name", name),
                            new KeyValuePair<string, string>("course", course),
                            new KeyValuePair<string, string>("venue", venue),
                            new KeyValuePair<string, string>("is_active", "true")
                        });

                var httpClient = new HttpClient();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                         System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true; // **** Always accept
                };

                httpClient.DefaultRequestHeaders.Add("Authorization", tokenField);
                var response = await httpClient.PostAsync("https://teenoh.webfactional.com/attendance_ish/", formContent);
                if (response.IsSuccessStatusCode)
                {

                    HttpContent content = response.Content;
                    var json = await content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<JObject>(json);
                    var NewAttendanceID = result.Value<int>("key");                    
                    httpClient.Dispose();

                    return NewAttendanceID.ToString();
                }
                else
                {
                    return null;
                }

            }


            catch (Exception e)
            {
                return null;
            }

        }


        public async Task<bool> StopAttendance(string token, string attendanceID)
        {
            var tokenField = "Token " + token;
            try
            {
                var formContent = new FormUrlEncodedContent(new[]
                {
                            new KeyValuePair<string, string>("attendance", attendanceID),
                            new KeyValuePair<string, string>("is_active", "False")
                        });

                var httpClient = new HttpClient();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                         System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true; // **** Always accept
                };

                httpClient.DefaultRequestHeaders.Add("Authorization", tokenField);
                var response = await httpClient.PostAsync("https://teenoh.webfactional.com/attendance_ish/", formContent);
                if (response.IsSuccessStatusCode)
                {

                    HttpContent content = response.Content;
                    var json = await content.ReadAsStringAsync();                  
                    httpClient.Dispose();

                    return true;
                }
                else
                {
                    return false;
                }

            }


            catch (Exception e)
            {
                return false;
            }

        }
    }
}
