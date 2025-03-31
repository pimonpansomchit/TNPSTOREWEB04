#nullable disable
using TNPSTOREWEB.Model;

namespace TNPSTOREWEB.Core
{
    public class GetDocRunning
    {
        public string GenDoc(int Docid, string ChkM, string ChkY, string docType, string dcid, string whid,string DBString,string wlid)
        {
            string StrSql = string.Empty;

            string run_no = string.Empty;
            string docno = string.Empty;
            int temprun;

            string ChkDoc = string.Empty;
            string PrvrunNo = string.Empty;
            GetDBConnect dB = new();
            string[] formatDoc;

            StrSql = " SELECT year_num,month_num,curr_num,num_digit,ISNULL(head_character,''),ISNULL(separator_char,''),ISNULL(format_doc,''),substring(year_num,3,2) FROM CTLdocrun " +
                     " WHERE doc_id =" + Docid +
                     " AND year_num='" + (ChkY).Replace(" ", "") + "'" +
                     " AND month_num='" + (ChkM).Replace(" ", "") + "'" +
                     " AND doc_type = '" + docType + "'" +
                     " AND dcid ='" + dcid + "'" +
                     " AND whid ='" + whid + "'"+
                     " AND wlid ='" + wlid + "'";

            if (dB.ExecuteReadData(StrSql, DBString))
            {
                if (dB.myReader.HasRows)
                {
                    while (dB.myReader.Read())
                    {

                        formatDoc = dB.myReader[6].ToString().Split('+');
                        int j = formatDoc.Length;

                        for (int i = 0; i < j; i++)
                        {

                            //head_character + year_num + month_num + curr_num

                            switch (formatDoc[i])
                            {
                               
                                case "head_character":
                                    docno = docno + dB.myReader[4].ToString().Replace(" ", "");
                                    break;
                                case "wlid":
                                    docno = docno + wlid.ToString().Replace(" ", "");
                                    break;
                                case "year_num":
                                    docno = docno + dB.myReader[0].ToString().Replace(" ", "");
                                    break;
                                case "year_num2":
                                    docno = docno + dB.myReader[7].ToString().Replace(" ", "");
                                    break;
                                case "month_num":
                                    docno = docno + dB.myReader[1].ToString().Replace(" ", "");
                                    break;
                                case "separator_char":
                                    docno = docno + dB.myReader[5].ToString().Replace(" ", "");
                                    break;
                            }
                        }
                        break;
                    }

                    temprun = Convert.ToInt32(dB.myReader[2].ToString()) + 1;
                    run_no = Convert.ToString(temprun.ToString(dB.myReader[3].ToString()));
                    docno = docno + run_no;
                    dB.CloseDB();

                    StrSql = "  UPDATE CTLdocrun SET   curr_num =" + temprun + " , doc_no ='" + docno.Replace(" ", "") + "'" +
                               " WHERE doc_type='" + docType + "'" +
                               " AND year_num = '" + (ChkY).Replace(" ", "") + "' AND month_num ='" + (ChkM).Replace(" ", "") + "'" +
                               " AND doc_id = " + Docid+
                               " AND wlid ='" + wlid + "'";

                    if (dB.ExecuteTransData(StrSql, DBString))
                    {
                        dB.CloseDB();

                    }
                }
                else
                {

                    docno = string.Empty;
                }

            }

            return docno.Replace(" ", "");
        }

    }
}
