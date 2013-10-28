using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Diagnostics;

namespace SentraUtility.Expression
{
    internal enum ePriority
    {
        None,
        Or,
        And,
        Not,
        Equality,
        PlusMinus,
        MulDiv,
        UnaryMinus
    }

    //[DebuggerNonUserCode]
    public class Evaluator
    {
        Tokenizer _Tokenizer;

        public Variables Variables = new Variables();
        public Objects ObjValues = new Objects();

        public object Parse(string strExpr)
        {
            _Tokenizer = new Tokenizer();
            _Tokenizer.Start(strExpr);
            _Tokenizer.NextToken();

            object Result = ParseExpr(ePriority.None);
            if (_Tokenizer.TokenType != eTokenType.end_of_expr)
                throw new ApplicationException("Ekspresi tidak lengkap !");
            return Result;
        }

        private object ParseExpr(ePriority ePriority)
        {
            object ValueLeft = null, ValueRight;

        LeftParse:
            #region LeftParse
            switch (_Tokenizer.TokenType)
            {
                case eTokenType.operator_minus:
                    _Tokenizer.NextToken();
                    ValueLeft = ParseExpr(ePriority.UnaryMinus);

                    if (ValueLeft.GetType() == typeof(decimal))
                        ValueLeft = -(decimal)ValueLeft;
                    else
                        throw new ApplicationException("Operator Unary Minus pada nilai non numerik");
                    break;
                case eTokenType.operator_plus:
                    _Tokenizer.NextToken();
                    goto LeftParse;
                case eTokenType.operator_not:
                    _Tokenizer.NextToken();
                    ValueLeft = ParseExpr(ePriority.Not);
                    if (ValueLeft.GetType() == typeof(bool))
                        ValueLeft = !(bool)ValueLeft;
                    else
                        throw new ApplicationException("Operator NOT pada nilai non boolean");
                    break;
                case eTokenType.value_identifier:
                    ValueLeft = ParseIdentifier();
                    break;
                case eTokenType.value_true:
                    ValueLeft = true;
                    _Tokenizer.NextToken();
                    break;
                case eTokenType.value_false:
                    ValueLeft = false;
                    _Tokenizer.NextToken();
                    break;
                case eTokenType.value_string:
                    ValueLeft = _Tokenizer.Value.ToString();
                    _Tokenizer.NextToken();
                    break;
                case eTokenType.value_number:
                    decimal TmpNum;
                    if (!decimal.TryParse(_Tokenizer.Value.ToString(),
                        NumberStyles.Number, BaseUtility.DefaultCultureInfo,
                        out TmpNum))
                        ValueLeft = decimal.Zero;
                    else
                        ValueLeft = TmpNum;
                    _Tokenizer.NextToken();
                    break;
                case eTokenType.value_date:
                    DateTime TmpDate;
                    if (!DateTime.TryParse(_Tokenizer.Value.ToString(),
                        BaseUtility.DefaultCultureInfo, DateTimeStyles.None,
                        out TmpDate))
                        ValueLeft = DateTime.Today;
                    else
                        ValueLeft = TmpDate;
                    _Tokenizer.NextToken();
                    break;
                case eTokenType.open_parenthesis:
                    _Tokenizer.NextToken();
                    ValueLeft = ParseExpr(ePriority.None);
                    if (_Tokenizer.TokenType == eTokenType.close_parenthesis)
                        _Tokenizer.NextToken();
                    else
                        throw new ApplicationException("Kurung Tutup tidak ditemukan !");
                    break;
            }
            #endregion

            if (ValueLeft == null) return null;

        RightParse:
            #region RightParse
            eTokenType tt = _Tokenizer.TokenType;
            switch (tt)
            {
                case eTokenType.end_of_expr:
                    return ValueLeft;
                case eTokenType.value_number:
                    throw new ApplicationException("Bilangan diikuti Bilangan tanpa Operator");
                case eTokenType.operator_plus:
                case eTokenType.operator_minus:
                    if (ePriority < ePriority.PlusMinus)
                    {
                        _Tokenizer.NextToken();
                        ValueRight = ParseExpr(ePriority.PlusMinus);
                        Type tp = ValueLeft.GetType();
                        if (tp == typeof(decimal))
                        {
                            Type tp2 = ValueRight.GetType();
                            if (tp2 == typeof(decimal))
                            {
                                if (tt == eTokenType.operator_plus)
                                    ValueLeft = (decimal)ValueLeft + (decimal)ValueRight;
                                else
                                    ValueLeft = (decimal)ValueLeft - (decimal)ValueRight;
                                goto RightParse;
                            }
                            else if (tp2 == typeof(DateTime))
                            {
                                if (tt == eTokenType.operator_plus)
                                    ValueLeft = ((DateTime)ValueRight).AddDays(
                                        Convert.ToDouble(ValueLeft));
                                goto RightParse;
                            }
                            else
                                throw new ApplicationException(
                                    "Operator Tambah/ Kurang pada Data bukan Bilangan/ DateTime");
                        }
                        else if (tp == typeof(DateTime))
                        {
                            Type tp2 = ValueRight.GetType();
                            if (tp2 == typeof(decimal))
                            {
                                if (tt == eTokenType.operator_plus)
                                    ValueLeft = ((DateTime)ValueLeft).AddDays(
                                        Convert.ToDouble(ValueRight));
                                else
                                    ValueLeft = ((DateTime)ValueLeft).AddDays(
                                        -Convert.ToDouble(ValueRight));
                                goto RightParse;
                            }
                            else
                                throw new ApplicationException(
                                    "Operator Tambah/ Kurang pada Data bukan Bilangan/ DateTime");
                        }
                        else if (tp == typeof(string))
                        {
                            ValueLeft = ValueLeft + ValueRight.ToString();
                            goto RightParse;
                        }
                        else
                            throw new ApplicationException(
                                "Operator Tambah/ Kurang pada Data bukan Bilangan/ DateTime");
                    }
                    break;
                case eTokenType.operator_mul:
                case eTokenType.operator_div:
                    if (ePriority < ePriority.MulDiv)
                    {
                        _Tokenizer.NextToken();
                        ValueRight = ParseExpr(ePriority.MulDiv);

                        if (ValueLeft.GetType() == typeof(decimal) &&
                            ValueRight.GetType() == typeof(decimal))
                        {
                            if (tt == eTokenType.operator_mul)
                                ValueLeft = (decimal)ValueLeft * (decimal)ValueRight;
                            else
                                ValueLeft = (decimal)ValueLeft / (decimal)ValueRight;
                            goto RightParse;
                        }
                        else
                            throw new ApplicationException("Operator Kali/ Bagi pada Data bukan Bilangan");
                    }
                    break;
                case eTokenType.operator_or:
                    if (ePriority < ePriority.Or)
                    {
                        _Tokenizer.NextToken();
                        ValueRight = ParseExpr(ePriority.Or);
                        if (ValueLeft.GetType() == typeof(bool) &&
                            ValueRight.GetType() == typeof(bool))
                        {
                            ValueLeft = (bool)ValueLeft || (bool)ValueRight;
                            goto RightParse;
                        }
                        else
                            throw new ApplicationException("Operator OR pada Data bukan Boolean");
                    }
                    break;
                case eTokenType.operator_and:
                    if (ePriority < ePriority.And)
                    {
                        _Tokenizer.NextToken();
                        ValueRight = ParseExpr(ePriority.And);
                        if (ValueLeft.GetType() == typeof(bool) &&
                            ValueRight.GetType() == typeof(bool))
                        {
                            ValueLeft = (bool)ValueLeft && (bool)ValueRight;
                            goto RightParse;
                        }
                        else
                            throw new ApplicationException("Operator AND pada Data bukan Boolean");
                    }
                    break;
                case eTokenType.operator_eq:
                    if (ePriority < ePriority.Equality)
                    {
                        _Tokenizer.NextToken();
                        ValueRight = ParseExpr(ePriority.Equality);

                        Type tp = ValueLeft.GetType();
                        if (!object.ReferenceEquals(tp, ValueRight.GetType()))
                            throw new ApplicationException("Membandingkan dua jenis data yang berbeda");
                        if (tp == typeof(decimal))
                            ValueLeft = (decimal)ValueLeft == (decimal)ValueRight;
                        else if (tp == typeof(string))
                            ValueLeft = (string)ValueLeft == (string)ValueRight;
                        else if (tp == typeof(DateTime))
                            ValueLeft = (DateTime)ValueLeft == (DateTime)ValueRight;
                        else if (tp == typeof(bool))
                            ValueLeft = (bool)ValueLeft == (bool)ValueRight;
                        goto RightParse;
                    }
                    break;
                case eTokenType.operator_ne:
                    if (ePriority < ePriority.Equality)
                    {
                        _Tokenizer.NextToken();
                        ValueRight = ParseExpr(ePriority.Equality);

                        Type tp = ValueLeft.GetType();
                        if (!object.ReferenceEquals(tp, ValueRight.GetType()))
                            throw new ApplicationException("Membandingkan dua jenis data yang berbeda");
                        if (tp == typeof(decimal))
                            ValueLeft = (decimal)ValueLeft != (decimal)ValueRight;
                        else if (tp == typeof(string))
                            ValueLeft = (string)ValueLeft != (string)ValueRight;
                        else if (tp == typeof(DateTime))
                            ValueLeft = (DateTime)ValueLeft != (DateTime)ValueRight;
                        else if (tp == typeof(bool))
                            ValueLeft = (bool)ValueLeft != (bool)ValueRight;
                        goto RightParse;
                    }
                    break;
                case eTokenType.operator_lt:
                    if (ePriority < ePriority.Equality)
                    {
                        _Tokenizer.NextToken();
                        ValueRight = ParseExpr(ePriority.Equality);

                        Type tp = ValueLeft.GetType();
                        if (!object.ReferenceEquals(tp, ValueRight.GetType()))
                            throw new ApplicationException("Membandingkan dua jenis data yang berbeda");
                        if (tp == typeof(decimal))
                            ValueLeft = (decimal)ValueLeft < (decimal)ValueRight;
                        else if (tp == typeof(string))
                            ValueLeft = string.Compare((string)ValueLeft, (string)ValueRight);
                        else if (tp == typeof(DateTime))
                            ValueLeft = (DateTime)ValueLeft < (DateTime)ValueRight;
                        else if (tp == typeof(bool))
                            ValueLeft = !(bool)ValueLeft && (bool)ValueRight;
                        goto RightParse;
                    }
                    break;
                case eTokenType.operator_le:
                    if (ePriority < ePriority.Equality)
                    {
                        _Tokenizer.NextToken();
                        ValueRight = ParseExpr(ePriority.Equality);

                        Type tp = ValueLeft.GetType();
                        if (!object.ReferenceEquals(tp, ValueRight.GetType()))
                            throw new ApplicationException("Membandingkan dua jenis data yang berbeda");
                        if (tp == typeof(decimal))
                            ValueLeft = (decimal)ValueLeft <= (decimal)ValueRight;
                        else if (tp == typeof(string))
                            ValueLeft = string.Compare((string)ValueLeft, (string)ValueRight);
                        else if (tp == typeof(DateTime))
                            ValueLeft = (DateTime)ValueLeft <= (DateTime)ValueRight;
                        else if (tp == typeof(bool))
                            ValueLeft = !(bool)ValueLeft && (bool)ValueRight ||
                                (bool)ValueLeft == (bool)ValueRight;
                        goto RightParse;
                    }
                    break;
                case eTokenType.operator_gt:
                    if (ePriority < ePriority.Equality)
                    {
                        _Tokenizer.NextToken();
                        ValueRight = ParseExpr(ePriority.Equality);

                        Type tp = ValueLeft.GetType();
                        if (!object.ReferenceEquals(tp, ValueRight.GetType()))
                            throw new ApplicationException("Membandingkan dua jenis data yang berbeda");
                        if (tp == typeof(decimal))
                            ValueLeft = (decimal)ValueLeft > (decimal)ValueRight;
                        else if (tp == typeof(string))
                            ValueLeft = string.Compare((string)ValueLeft, (string)ValueRight);
                        else if (tp == typeof(DateTime))
                            ValueLeft = (DateTime)ValueLeft > (DateTime)ValueRight;
                        else if (tp == typeof(bool))
                            ValueLeft = (bool)ValueLeft && !(bool)ValueRight;
                        goto RightParse;
                    }
                    break;
                case eTokenType.operator_ge:
                    if (ePriority < ePriority.Equality)
                    {
                        _Tokenizer.NextToken();
                        ValueRight = ParseExpr(ePriority.Equality);

                        Type tp = ValueLeft.GetType();
                        if (!object.ReferenceEquals(tp, ValueRight.GetType()))
                            throw new ApplicationException("Membandingkan dua jenis data yang berbeda");
                        if (tp == typeof(decimal))
                            ValueLeft = (decimal)ValueLeft >= (decimal)ValueRight;
                        else if (tp == typeof(string))
                            ValueLeft = string.Compare((string)ValueLeft, (string)ValueRight);
                        else if (tp == typeof(DateTime))
                            ValueLeft = (DateTime)ValueLeft >= (DateTime)ValueRight;
                        else if (tp == typeof(bool))
                            ValueLeft = (bool)ValueLeft && !(bool)ValueRight ||
                                (bool)ValueLeft == (bool)ValueRight;
                        goto RightParse;
                    }
                    break;
            }
            #endregion

            return ValueLeft;
        }
        private object ParseIdentifier()
        {
            string ObjName = _Tokenizer.Value.ToString();
            _Tokenizer.NextToken();
            if (_Tokenizer.TokenType == eTokenType.dot)
            {
                _Tokenizer.NextToken();
                if (_Tokenizer.TokenType != eTokenType.value_identifier)
                    throw new ApplicationException(
                        "Error Akses Member pada " + ObjName);
                string MemberName = _Tokenizer.Value.ToString();
                _Tokenizer.NextToken();
                if (_Tokenizer.TokenType == eTokenType.open_parenthesis)
                {
                    List<object> LParams = new List<object>();
                    do
                    {
                        _Tokenizer.NextToken();
                        LParams.Add(ParseExpr(ePriority.None));
                        switch (_Tokenizer.TokenType)
                        {
                            case eTokenType.comma:
                                break;
                            case eTokenType.close_parenthesis:
                                goto EndExpr;
                        }
                    } while (_Tokenizer.TokenType !=
                        eTokenType.end_of_expr);
                EndExpr:
                    _Tokenizer.NextToken();
                    if (LParams.Count == 1 && LParams[0] == null)
                        LParams.Clear();
                    return ObjValues.GetValue(ObjName,
                        MemberName, LParams.ToArray());
                }
                else
                {
                    return ObjValues.GetValue(
                        ObjName, MemberName);
                }
            }
            else if (_Tokenizer.TokenType == eTokenType.open_parenthesis)
            {
                // Fungsi Umum
                List<object> LParams = new List<object>();
                do
                {
                    _Tokenizer.NextToken();
                    LParams.Add(ParseExpr(ePriority.None));
                    switch (_Tokenizer.TokenType)
                    {
                        case eTokenType.comma:
                            break;
                        case eTokenType.close_parenthesis:
                            goto EndExpr;
                    }
                } while (_Tokenizer.TokenType !=
                    eTokenType.end_of_expr);
            EndExpr:
                _Tokenizer.NextToken();
                if (LParams.Count == 1 && LParams[0] == null)
                    LParams.Clear();
                return ObjValues.GetValue(string.Empty,
                    ObjName, LParams.ToArray());
            }
            object VarValue = Variables.GetValue(ObjName);
            if (VarValue == null)
                VarValue = ObjValues.GetValue(string.Empty, ObjName);

            return VarValue;
        }
    }
}
