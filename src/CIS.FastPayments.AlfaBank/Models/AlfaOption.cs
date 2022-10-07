namespace CIS.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Настройки ЦРПТ провайдера.
    /// </summary>
    public class AlfaOption
    {
        public static readonly AlfaOption DemoOption = new()
        {
            ServiceUrl = "http://217.12.103.147:9914/fsCryptoProxy",
            Certificate = new Certificate
            {
                Name = "Тестовый сертификат",
                AlfaAlias = "c2b-pos-test",
                PrivateKey = @"-----BEGIN PRIVATE KEY-----
MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCnQuZHudOU942u
DDuvI3io50Ern/VcnLg6fmaANZleJtnFk967wYcwL984n1UxAYVmCKLV2J+OVuG7
tHbKzwm8oYT/DQpRvr2EoUU8my8nRpiIfjGw94hnWMc4o/fB07ALO+2MVTaBJRGl
GZ9sw32hC/VYSzkxfQ8DaHgMde3HIMWRld2UjZX81lXLZxmO40epoYA1nj+xBFjA
uHAn7ehmeE0/536WRCxbQjCLxhjOZCcTVvV9TSAqBGMgu0RlHf1GDVPPyQVgjKcV
Zp3tLMbBQ/HoT41U95OMz55xCLm9VTh6jrPPl4zF9VfmpUaT2VJp037tVGYlpazk
YHGeM3/xAgMBAAECggEAFXTqVCy1RRCtyvRUSr8m6SoDbPx/7iHHq29QKGXX1yFu
Qd+ajOUCee7GOL4N4HPEwGZet0arD0g7fIglzhYZtmpVBKAQYRSyPLuwhVoWBjiS
9D2WE9DpXXKg2fYE4EdO99339w7uZ0pRVWfwUPRSHzEIyAscWuojXSlPby/IsUjg
SZnxvjnpbHNsg/YhW5DrqY6yXTcrKxytQp2oagcF1aqtGknMbEy8C+fQ3YbpeVfv
vvbr/sq+FpuZ0WLA/rmsjKgmA/umVFbWxcbY58ouYX+k41/3zdF3v9x3ISQqJD2S
nMX94r9mgypc8xT3L58vW770c3IgogQs5yGlx/x2bQKBgQDnoTs61HXXyBnjHl+L
yzziw6xL14+Fwt1W4X5H9KC0MRc2LE0q5uRt7PCJCWMwiL+LzFvSL5+K+myHiW1g
D17abnqn7CcHeiE5FUbnVOyij8ReyST42TX2YKQnp+WLXQVro+F7+y1XgQpSwEdm
llLi5gSQ1TTiw3Vwi4AgqPWnDQKBgQC42/QCUd/krwMrPH1kjz6svtlmCKzLmRCX
q0RTjjBZZ6jKnjDlL9w2ah7+HwruDLweZ7t9v5V8PJzFJGohV4uuv9TJxZLdKL31
9K+wqwUxWY/iUkX05ua5BD5QErAWTQPiqR7SZNCaa/tBCAK+1dhgfW1Y6mCY9eYv
G3rl2p0DdQKBgHFPkmYJIUzZ+q/8X8lHNxXHmzXO0cshtJ2X2erhZOxBR5WcvnNK
WFzeGB0xlnBbtThyVEjSOz4Fm6oCNJtVaZV5jk8vWzhxKmd+XR01kj1ED1A/HfMO
bzODu0oa/FPcwZYSqW+5REkWOzKYFVW+G/YbiAhCBIkaDQ1tcNSJUwLRAoGAAi+f
sBNVyXeEWxOJDBJhlFthaMJ7gKDbwF9nHHHXAoemSFccjulE+mPA4BJv56bA5r8l
SXRliSWSWQZ4NtK5NSTRmF08wl7D+E+fcEBlfFLpz6xXZXEBk3iIYBwIdwsMG7cS
RTyp9tmGDBvTJHyU5Xc2PtHuuIBX64CwsF0odC0CgYEA02EZY2lXBxkYGAsT4/fb
KRBoLLB6HCztHi41nEo/zsuMDYNFKYfQSxHhazZwSBkYXEU12W6w6tI9UdyiZX7q
xxuWhbezvABjbEdiAqPjC/F7zm9tMqKHrQiYOb+eQOVAxch6oqnn5Qe5bXBf29bX
siKItM2C/cXIjAJMWxIdQsw=
-----END PRIVATE KEY-----",

                PublicBody = @"-----BEGIN CERTIFICATE-----
MIIDuDCCAqCgAwIBAgIEFuLAnTANBgkqhkiG9w0BAQsFADCBnTELMAkGA1UEBhMC
UlUxDzANBgNVBAgTBk1vc2NvdzEPMA0GA1UEBxMGTW9zY293MRYwFAYDVQQKEw1K
U0MgQWxmYS1CYW5rMRowGAYDVQQLExFQcm9jZXNzaW5nIGNlbnRlcjEVMBMGA1UE
AxMMcG9zLWMyYi10ZXN0MSEwHwYJKoZIhvcNAQkBFhJwY19zc2xAYWxmYWJhbmsu
cnUwHhcNMjIwMzAzMDcxOTU3WhcNMjMwMzAzMDcxOTU3WjCBnTELMAkGA1UEBhMC
UlUxDzANBgNVBAgTBk1vc2NvdzEPMA0GA1UEBxMGTW9zY293MRYwFAYDVQQKEw1K
U0MgQWxmYS1CYW5rMRowGAYDVQQLExFQcm9jZXNzaW5nIGNlbnRlcjEVMBMGA1UE
AxMMcG9zLWMyYi10ZXN0MSEwHwYJKoZIhvcNAQkBFhJwY19zc2xAYWxmYWJhbmsu
cnUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCnQuZHudOU942uDDuv
I3io50Ern/VcnLg6fmaANZleJtnFk967wYcwL984n1UxAYVmCKLV2J+OVuG7tHbK
zwm8oYT/DQpRvr2EoUU8my8nRpiIfjGw94hnWMc4o/fB07ALO+2MVTaBJRGlGZ9s
w32hC/VYSzkxfQ8DaHgMde3HIMWRld2UjZX81lXLZxmO40epoYA1nj+xBFjAuHAn
7ehmeE0/536WRCxbQjCLxhjOZCcTVvV9TSAqBGMgu0RlHf1GDVPPyQVgjKcVZp3t
LMbBQ/HoT41U95OMz55xCLm9VTh6jrPPl4zF9VfmpUaT2VJp037tVGYlpazkYHGe
M3/xAgMBAAEwDQYJKoZIhvcNAQELBQADggEBAF9a+bpAC3lxDK3dA6+zYDyUtYIA
Lt2au35dbJGublNGWPHS22qETMT7LNfq+DkG14rkqDw8vT1Z5HyL2JWbZA7a/lXm
KK2i796cMZYGoeRvRgVM2fBAWn8+DMxHsoIfb/pRsylZV9BRKqo2LtrTcmkPIMiC
JVTObw37p6uV0ZbS0LdnLoR7GVitU7iatS3Ioe5yZKfinon3d+IZhqrlyZv9i+9O
4mnX9o/P1g0JBDpHAW/tYS/7JFd/hl9xaSupzes3444bOH7HtWSTttZxA8m+cMzU
1XkcyrJtZI7Q6S3QiInwboHJFge7uuq00DzNAM/vO6Aq76cfEx83WmU/YLI=
-----END CERTIFICATE-----"
            }
        };


 
        /// <summary>
        /// Адрес сервиса Альфа-Банка.
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Сертификат для подписи запроса.
        /// </summary>
        public Certificate Certificate { get; set; }
    }
}
