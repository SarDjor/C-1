using System;

class Frustum
{
    static Frustum()
    {
        Num = 0;
    }
    public Frustum(double UR, double DR, double H)
    {
        URadius = UR;
        DRadius = DR;
        Height = H;
        Num++;
        ID=(UR*3)+(DR*1.5)+(H*2.1);
    }
    public Frustum()
    {
        URadius = 0;
        DRadius = 0;
        Height = 0;
        Num++;
        ID = 0;
            }

    
    public double URadius { get; set; }
    public double DRadius { get; set; }
    public double Height { get; set; }

    private static int Num;
    private readonly double ID;
    private const double Pi = 3.14;

    public static void ClassInfo()
    {
        Console.WriteLine("\nClassname: Frustum\nNumber of objects: {0}\n", Num);
    }

        public void ShowNum()
    {
        Console.WriteLine("Numbers of objects: {0}", Num);
    }

    public double Capacity ()                                                //объем усеченного конуса 
    {
        return (1 / 3) * Pi * Height * (DRadius * DRadius + URadius * DRadius + URadius * URadius);
    }

    public double OriginalCapacity(ref double OriginalH)                    //объем конуса
    {
        return (Capacity() / Height) * OriginalH;
    }

    public void Test(out double r1, out double r2) {                        //пробный класс для out
        r1 = this.URadius;
        r2 = this.DRadius;
    }

    public override bool Equals(object obj)
    {
        if (obj is Frustum && obj != null)
        {
            Frustum temp;
            temp = (Frustum)obj;
            if (temp.DRadius == this.DRadius
             && temp.URadius == this.URadius
             && temp.Height == this.Height)
                return true;
            else return false;
        }
        else return false;
    }

    public override int GetHashCode()
    {
        return (int)this.ID;
    }
}


static class FrustumMath
{
    public static double CircleSquare(double r){
        return 3.14 * r * r;
    }

    public static double LandSquare(Frustum obj) {
        double L = (obj.DRadius-obj.URadius)*(obj.DRadius-obj.URadius)+obj.Height*obj.Height;
        return 3.14 * (obj.DRadius + obj.URadius) * L;
    }

    public static double FullSquare(Frustum obj)
    {
        return CircleSquare(obj.URadius) + CircleSquare(obj.DRadius) + LandSquare(obj);
    }

    public static bool CheckBox(Frustum obj,double a,double b,double c) {
        if (obj.DRadius <= a && obj.Height <= c) return true;
        if (obj.DRadius <= c && obj.Height <= a) return true;
        if (obj.DRadius <= a && obj.Height <= b) return true;
        return false;
    }
}
class Program
    {

        static void Main(string[] args)
        {
        Frustum fr = new Frustum(1, 1, 1);
        Frustum fr2 = new Frustum();
        fr.ShowNum();
        Frustum.ClassInfo();
        
            Object temp = fr;
            if (temp.Equals(fr)) Console.WriteLine("Temp и fr Одинаковы");
            Console.WriteLine("fr ID: {0}",fr.GetHashCode());

            Console.WriteLine("******Demonstrarion of FrustumMath class with fr******");
            Console.WriteLine("Land Square of fr with LandSquare() = {0}",FrustumMath.LandSquare(fr));
            Console.WriteLine("Full Square of fr with FullSquare() = {0}",FrustumMath.FullSquare(fr));
            Console.WriteLine("CheckBox(fr,3,3,3) = {0}", FrustumMath.CheckBox(fr,3,3,3));
            Console.WriteLine("CheckBox(fr,0.3,0.3,3) = {0}", FrustumMath.CheckBox(fr, 0.3, 0.3, 3));

            Console.WriteLine("\n\n");
            Console.WriteLine("*******Anonumoys type*******");
            var Anoniym = new {URadius = 2, DRadius = 2, Height = 2};
            Console.WriteLine("Anoniym.URadius = {0}\nAnoniym.DRadius = {1}\nAnoniym.Height = {2}\nAnoniym.GetHashCode = {3}", Anoniym.URadius, Anoniym.DRadius, Anoniym.Height,Anoniym.GetHashCode());
 
        }
    }

