using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elem = ClassLibraryClustering.ClassElements.elemArr;

namespace UnitTestProjectSeasonalCoefficients
{
    class ClassForTest
    {
        public static List<elem> DataTest()
        {
            return new List<elem>()
            {
new elem("JAN 1900", DateTime.Parse("01.01.1900"),1,2.000),
new elem("FEB 1900", DateTime.Parse("01.02.1900"),2,2.000),
new elem("MAR 1900", DateTime.Parse("01.03.1900"),3,2.000),
new elem("APR 1900", DateTime.Parse("01.04.1900"),4,6.000),
new elem("MAY 1900", DateTime.Parse("01.05.1900"),5,1.000),
new elem("JUN 1900", DateTime.Parse("01.06.1900"),6,3.000),
new elem("JUL 1900", DateTime.Parse("01.07.1900"),7,4.000),
new elem("AUG 1900", DateTime.Parse("01.08.1900"),8,4.000),
new elem("SEP 1900", DateTime.Parse("01.09.1900"),9,1.000),
new elem("OCT 1900", DateTime.Parse("01.10.1900"),10,3.000),
new elem("NOV 1900", DateTime.Parse("01.11.1900"),11,2.000),
new elem("DEC 1900", DateTime.Parse("01.12.1900"),12,4.000),
new elem("JAN 1901", DateTime.Parse("01.01.1901"),13,4.000),
new elem("FEB 1901", DateTime.Parse("01.02.1901"),14,3.000),
new elem("MAR 1901", DateTime.Parse("01.03.1901"),15,6.000),
new elem("APR 1901", DateTime.Parse("01.04.1901"),16,1.000),
new elem("MAY 1901", DateTime.Parse("01.05.1901"),17,2.000),
new elem("JUN 1901", DateTime.Parse("01.06.1901"),18,2.000),
new elem("JUL 1901", DateTime.Parse("01.07.1901"),19,3.000),
new elem("AUG 1901", DateTime.Parse("01.08.1901"),20,1.000),
new elem("SEP 1901", DateTime.Parse("01.09.1901"),21,5.000),
new elem("OCT 1901", DateTime.Parse("01.10.1901"),22,3.000),
new elem("NOV 1901", DateTime.Parse("01.11.1901"),23,4.000),
new elem("DEC 1901", DateTime.Parse("01.12.1901"),24,1.000),
new elem("JAN 1902", DateTime.Parse("01.01.1902"),25,1.000),
new elem("FEB 1902", DateTime.Parse("01.02.1902"),26,3.000),
new elem("MAR 1902", DateTime.Parse("01.03.1902"),27,3.000),
new elem("APR 1902", DateTime.Parse("01.04.1902"),28,7.000),
new elem("MAY 1902", DateTime.Parse("01.05.1902"),29,3.000),
new elem("JUN 1902", DateTime.Parse("01.06.1902"),30,3.000),
new elem("JUL 1902", DateTime.Parse("01.07.1902"),31,7.000),
new elem("AUG 1902", DateTime.Parse("01.08.1902"),32,5.000),
new elem("SEP 1902", DateTime.Parse("01.09.1902"),33,2.000),
new elem("OCT 1902", DateTime.Parse("01.10.1902"),34,8.000),
new elem("NOV 1902", DateTime.Parse("01.11.1902"),35,5.000),
new elem("DEC 1902", DateTime.Parse("01.12.1902"),36,3.000),
new elem("JAN 1903", DateTime.Parse("01.01.1903"),37,3.000),
new elem("FEB 1903", DateTime.Parse("01.02.1903"),38,4.000),
new elem("MAR 1903", DateTime.Parse("01.03.1903"),39,2.000),
new elem("APR 1903", DateTime.Parse("01.04.1903"),40,3.000),
new elem("MAY 1903", DateTime.Parse("01.05.1903"),41,5.000),
new elem("JUN 1903", DateTime.Parse("01.06.1903"),42,4.000),
new elem("JUL 1903", DateTime.Parse("01.07.1903"),43,4.000),
new elem("AUG 1903", DateTime.Parse("01.08.1903"),44,2.000),
new elem("SEP 1903", DateTime.Parse("01.09.1903"),45,5.000),
new elem("OCT 1903", DateTime.Parse("01.10.1903"),46,4.000),
new elem("NOV 1903", DateTime.Parse("01.11.1903"),47,5.000),
new elem("DEC 1903", DateTime.Parse("01.12.1903"),48,4.000),
new elem("JAN 1904", DateTime.Parse("01.01.1904"),49,1.000),
new elem("FEB 1904", DateTime.Parse("01.02.1904"),50,2.000),
new elem("MAR 1904", DateTime.Parse("01.03.1904"),51,4.000),
new elem("APR 1904", DateTime.Parse("01.04.1904"),52,2.000),
new elem("MAY 1904", DateTime.Parse("01.05.1904"),53,3.000),
new elem("JUN 1904", DateTime.Parse("01.06.1904"),54,6.000),
new elem("JUL 1904", DateTime.Parse("01.07.1904"),55,2.000),
new elem("AUG 1904", DateTime.Parse("01.08.1904"),56,7.000),
new elem("SEP 1904", DateTime.Parse("01.09.1904"),57,5.000),
new elem("OCT 1904", DateTime.Parse("01.10.1904"),58,6.000)
            };

        }
    }
}
