using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

public class Example
{
    public static async Task Main()
    {
        var l = new List<string>();
        var stopwatch = new Stopwatch();
        var source = new char[16] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f' };
        var salt = new byte[4];
        var md5 = MD5.Create();
        var exceptionCount = 0;
        ulong total = 0;

        stopwatch.Start();
        Parallel.For(0, source.Length, i =>
        {
            Parallel.For(0, source.Length, i1 =>
            {
                Parallel.For(0, source.Length, i2 =>
                {
                    Parallel.For(0, source.Length, i3 =>
                    {
                        Parallel.For(0, source.Length, i4 =>
                        {
                            Parallel.For(0, source.Length, i5 =>
                            {
                                Parallel.For(0, source.Length, i6 =>
                                {
                                    Parallel.For(0, source.Length, i7 =>
                                    {
                                        try
                                        {
                                            var sb = new StringBuilder();

                                            var d = new char[8] { source[i], source[i1], source[i2], source[i3], source[i4], source[i5], source[i6], source[i7] };
                                            //sb.Clear();
                                            //var d = new char[4] { source[i], source[i1], source[i2], source[i3] };
                                            var str = new string(d);
                                            //sb.AppendLine(str);
                                            var p = new string(d);
                                            //perm.Add(sb.ToString());
                                            var hash = new Platform.Kernel.Cryptography.Md4Hash().ComputeHash(str);
                                            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(hash);
                                            var base64 = System.Convert.ToBase64String(plainTextBytes);
                                            sb.AppendFormat("psw - {0} - hash - {1}. Elapsed time: {2}. Generated: {3}", str, base64, stopwatch.Elapsed, Interlocked.Increment(ref total));
                                            //Debug.WriteLine("psw: " + str + " hash: " + base64 + ". Total:" + Interlocked.Increment(ref total) + " time elapsed: " + stopwatch.Elapsed);
#if DEBUG
                                            Debug.WriteLine(sb.ToString());
#else
                                            Console.WriteLine(sb.ToString());
#endif
                                        }

                                        catch (Exception ex)
                                        {
                                            ++exceptionCount;
#if DEBUG
                                            Debug.WriteLine("{0} - {1}. Exceptions:{2}", ex.Message, total, exceptionCount);
#else
                                            Console.WriteLine("{0} - {1}. Exceptions:{2}", ex.Message, total, exceptionCount);
#endif

                                        }
                                    });
                                });
                            });
                        });
                    });
                });
            });
        });
        stopwatch.Stop();
    }
}
