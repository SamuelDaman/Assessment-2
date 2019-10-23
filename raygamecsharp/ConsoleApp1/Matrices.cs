using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Matrix2
    {
        public static Matrix2 identity = new Matrix2(1, 0, 0, 1);
        public float m1, m2, m3, m4;
        public Matrix2(float val1, float val2, float val3, float val4)
        {
            m1 = val1; m2 = val2;
            m3 = val3; m4 = val4;
        }
        public Matrix2()
        {
            m1 = 1; m2 = 0;
            m3 = 0; m4 = 0;
        }
        Matrix2 GetTransposed()
        {
            return new Matrix2
            (
                m1, m3,
                m2, m4
            );
        }
        void SetScaled(float x, float y)
        {
            m1 = x; m2 = 0;
            m3 = 0; m4 = y;
        }
        void Scale(float x, float y)
        {
            Matrix2 m = new Matrix2();
            m.SetScaled(x, y);
            Set(this * m);
        }
        public static Matrix2 operator *(Matrix2 lhs, Matrix2 rhs)
        {
            return new Matrix2
            (
                lhs.m1 * rhs.m1 + lhs.m3 * rhs.m2,
                lhs.m2 * rhs.m1 + lhs.m4 * rhs.m2,

                lhs.m1 * rhs.m3 + lhs.m3 * rhs.m4,
                lhs.m2 * rhs.m3 + lhs.m4 * rhs.m4
            );

        }
        void Set(Matrix2 m)
        {
            m1 = m.m1; m2 = m.m2;
            m3 = m.m3; m4 = m.m4;
        }
        void Set(float val1, float val2, float val3, float val4)
        {
            m1 = val1; m2 = val2;
            m3 = val3; m4 = val4;
        }
        public void SetRotate(float radians)
        {
            Set
            (
                (float)Math.Cos(radians), (float)-Math.Sin(radians),
                (float)Math.Sin(radians), (float)Math.Cos(radians)
            );
        }
        public void Rotate(float radians)
        {
            Matrix2 m = new Matrix2();
            m.SetRotate(radians);
            Set(this * m);
        }
    }
    public class Matrix3
    {
        public static Matrix3 identity = new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1);
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9;
        public Matrix3(float val1, float val2, float val3, float val4, float val5, float val6, float val7, float val8, float val9)
        {
            m1 = val1; m2 = val2; m3 = val3;
            m4 = val4; m5 = val5; m6 = val6;
            m7 = val7; m8 = val8; m9 = val9;
        }
        public Matrix3()
        {
            m1 = 1; m2 = 0; m3 = 0;
            m4 = 0; m5 = 1; m6 = 0;
            m7 = 0; m8 = 0; m9 = 1;
        }
        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3
            (
                lhs.m1 * rhs.m1 + lhs.m4 * rhs.m2 + lhs.m7 * rhs.m3,
                lhs.m2 * rhs.m1 + lhs.m5 * rhs.m2 + lhs.m8 * rhs.m3,
                lhs.m3 * rhs.m1 + lhs.m6 * rhs.m2 + lhs.m9 * rhs.m3,

                lhs.m1 * rhs.m4 + lhs.m4 * rhs.m5 + lhs.m7 * rhs.m6,
                lhs.m2 * rhs.m4 + lhs.m5 * rhs.m5 + lhs.m8 * rhs.m6,
                lhs.m3 * rhs.m4 + lhs.m6 * rhs.m5 + lhs.m9 * rhs.m6,

                lhs.m1 * rhs.m7 + lhs.m4 * rhs.m8 + lhs.m7 * rhs.m9,
                lhs.m2 * rhs.m7 + lhs.m5 * rhs.m8 + lhs.m8 * rhs.m9,
                lhs.m3 * rhs.m7 + lhs.m6 * rhs.m8 + lhs.m9 * rhs.m9
            );
        }
        Matrix3 GetTransposed()
        {
            return new Matrix3
            (
                m1, m4, m7,
                m2, m5, m8,
                m3, m6, m9
            );
        }
        public void SetScaled(float x, float y, float z)
        {
            m1 = x; m2 = 0; m3 = 0;
            m4 = 0; m5 = y; m6 = 0;
            m7 = 0; m8 = 0; m9 = z;
        }
        void Scale(float x, float y, float z)
        {
            Matrix3 m = new Matrix3();
            m.SetScaled(x, y, z);
            Set(this * m);
        }
        void Set(Matrix3 m)
        {
            m1 = m.m1; m2 = m.m2; m3 = m.m3;
            m4 = m.m4; m5 = m.m5; m6 = m.m6;
            m7 = m.m7; m8 = m.m8; m9 = m.m9;
        }
        void Set(float val1, float val2, float val3, float val4, float val5, float val6, float val7, float val8, float val9)
        {
            m1 = val1; m2 = val2; m3 = val3;
            m4 = val4; m5 = val5; m6 = val6;
            m7 = val7; m8 = val8; m9 = val9;
        }
        public void SetRotateX(double radians)
        {
            Set
            (
                1, 0, 0,
                0, (float)Math.Cos(radians), (float)Math.Sin(radians),
                0, (float)-Math.Sin(radians), (float)Math.Cos(radians)
            );
        }
        public void SetRotateY(double radians)
        {
            Set
            (
                (float)Math.Cos(radians), 0, (float)-Math.Sin(radians),
                0, 1, 0,
                (float)Math.Sin(radians), 0, (float)Math.Cos(radians)
            );
        }
        public void SetRotateZ(double radians)
        {
            Set
            (
                (float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                (float)-Math.Sin(radians), (float)Math.Cos(radians), 0,
                0, 0, 1
            );
        }
        public void RotateX(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateX(radians);
            Set(this * m);
        }
        public void RotateY(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateY(radians);
            Set(this * m);
        }
        public void RotateZ(double radians)
        {
            Matrix3 m = new Matrix3();
            m.SetRotateZ(radians);
            Set(this * m);
        }
        public void SetTranslation(float x, float y)
        {
            m7 = x; m8 = y; m9 = 1;
        }
        public void Translate(float x, float y)
        {
            m7 += x; m8 += y;
        }
        void SetEuler(float pitch, float yaw, float roll)
        {
            Matrix3 x = new Matrix3();
            Matrix3 y = new Matrix3();
            Matrix3 z = new Matrix3();
            x.SetRotateX(pitch);
            y.SetRotateY(yaw);
            z.SetRotateZ(roll);
            Set(z * y * x);
        }
    }
    public class Matrix4
    {
        public float m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16;
        public Matrix4(float val1, float val2, float val3, float val4, float val5, float val6, float val7, float val8, float val9, float val10, float val11, float val12, float val13, float val14, float val15, float val16)
        {
            m1 = val1; m2 = val2; m3 = val3; m4 = val4;
            m5 = val5; m6 = val6; m7 = val7; m8 = val8;
            m9 = val9; m10 = val10; m11 = val11; m12 = val12;
            m13 = val13; m14 = val14; m15 = val15; m16 = val16;
        }
        public static Matrix4 CreateIdentity()
        {
            return new Matrix4
            (
                1f, 0f, 0f, 0f,
                0f, 1f, 0f, 0f,
                0f, 0f, 1f, 0f,
                0f, 0f, 0f, 1f
            );
        }
        public void Set(Matrix4 m)
        {
            m1 = m.m1; m2 = m.m2; m3 = m.m3; m4 = m.m4;
            m5 = m.m5; m6 = m.m6; m7 = m.m7; m8 = m.m8;
            m9 = m.m9; m10 = m.m10; m11 = m.m11; m12 = m.m12;
            m13 = m.m13; m14 = m.m14; m15 = m.m15; m16 = m.m16;
        }
        public void Set(float val1, float val2, float val3, float val4, float val5, float val6, float val7, float val8, float val9, float val10, float val11, float val12, float val13, float val14, float val15, float val16)
        {
            m1 = val1; m2 = val2; m3 = val3; m4 = val4;
            m5 = val5; m6 = val6; m7 = val7; m8 = val8;
            m9 = val9; m10 = val10; m11 = val11; m12 = val12;
            m13 = val13; m14 = val14; m15 = val15; m16 = val16;
        }
        public void SetScaled(float x, float y, float z, float w)
        {
            m1 = x; m2 = 0; m3 = 0; m4 = 0;
            m5 = 0; m6 = y; m7 = 0; m8 = 0;
            m9 = 0; m10 = 0; m11 = z; m12 = 0;
            m13 = 0; m14 = 0; m15 = 0; m16 = w;
        }
        public void SetRotateX(double radians)
        {
            Set
            (
                1, 0, 0, 0,
                0, (float)Math.Cos(radians), (float)Math.Sin(radians), 0,
                0, (float)-Math.Sin(radians), (float)Math.Cos(radians), 0,
                0, 0, 0, 1
            );
        }
        public void SetTranslation(float x, float y, float z)
        {
            m13 = x; m14 = y; m15 = z; m16 = 1;
        }
        void Translate(float x, float y, float z)
        {
            m13 += x; m14 += y; m15 += z;
        }
        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
            (
                lhs.m1 * rhs.m1 + lhs.m5 * rhs.m2 + lhs.m9 * rhs.m3 + lhs.m13 * rhs.m4,
                lhs.m2 * rhs.m1 + lhs.m6 * rhs.m2 + lhs.m10 * rhs.m3 + lhs.m14 * rhs.m4,
                lhs.m3 * rhs.m1 + lhs.m7 * rhs.m2 + lhs.m11 * rhs.m3 + lhs.m15 * rhs.m4,
                lhs.m4 * rhs.m1 + lhs.m8 * rhs.m2 + lhs.m12 * rhs.m3 + lhs.m16 * rhs.m4,

                lhs.m1 * rhs.m5 + lhs.m5 * rhs.m6 + lhs.m9 * rhs.m7 + lhs.m13 * rhs.m8,
                lhs.m2 * rhs.m5 + lhs.m6 * rhs.m6 + lhs.m10 * rhs.m7 + lhs.m14 * rhs.m8,
                lhs.m3 * rhs.m5 + lhs.m7 * rhs.m6 + lhs.m11 * rhs.m7 + lhs.m15 * rhs.m8,
                lhs.m4 * rhs.m5 + lhs.m8 * rhs.m6 + lhs.m12 * rhs.m7 + lhs.m16 * rhs.m8,

                lhs.m1 * rhs.m9 + lhs.m5 * rhs.m10 + lhs.m9 * rhs.m11 + lhs.m13 * rhs.m12,
                lhs.m2 * rhs.m9 + lhs.m6 * rhs.m10 + lhs.m10 * rhs.m11 + lhs.m14 * rhs.m12,
                lhs.m3 * rhs.m9 + lhs.m7 * rhs.m10 + lhs.m11 * rhs.m11 + lhs.m15 * rhs.m12,
                lhs.m4 * rhs.m9 + lhs.m8 * rhs.m10 + lhs.m12 * rhs.m11 + lhs.m16 * rhs.m12,

                lhs.m1 * rhs.m13 + lhs.m5 * rhs.m14 + lhs.m9 * rhs.m15 + lhs.m13 * rhs.m16,
                lhs.m2 * rhs.m13 + lhs.m6 * rhs.m14 + lhs.m10 * rhs.m15 + lhs.m14 * rhs.m16,
                lhs.m3 * rhs.m13 + lhs.m7 * rhs.m14 + lhs.m11 * rhs.m15 + lhs.m15 * rhs.m16,
                lhs.m4 * rhs.m13 + lhs.m8 * rhs.m14 + lhs.m12 * rhs.m15 + lhs.m16 * rhs.m16
            );
        }
    }
    //struct Vector3
    //{
    //    public float x, y, z;
    //    public Vector3(float xVal, float yVal, float zVal)
    //    {
    //        x = xVal;
    //        y = yVal;
    //        z = zVal;
    //    }
    //    public void Normalize()
    //    {
    //        float m = Magnitude();
    //        this.x /= m;
    //        this.y /= m;
    //        this.z /= m;
    //    }
    //    public float Magnitude()
    //    {
    //        return (float)Math.Sqrt(x * x + y * y + z * z);
    //    }
    //    public float MagnitudeSqr()
    //    {
    //        return (x * x + y * y + z * z);
    //    }
    //    public float Distance(Vector3 other)
    //    {
    //        float diffX = x - other.x;
    //        float diffY = y - other.y;
    //        float diffZ = z - other.z;
    //        return (float)Math.Sqrt(diffX * diffX + diffY * diffY + diffZ * diffZ);
    //    }
    //    public float DistanceSqr(Vector3 other)
    //    {
    //        float diffX = x - other.x;
    //        float diffY = y - other.y;
    //        float diffZ = z - other.z;
    //        return diffX * diffX + diffY * diffY + diffZ * diffZ;
    //    }
    //    public float Dot(Vector3 rhs)
    //    {
    //        return x * rhs.x + y * rhs.y + z * rhs.z;
    //    }
    //    public static Vector3 operator *(Matrix3 lhs, Vector3 rhs)
    //    {
    //        return new Vector3
    //        (
    //            (lhs.m1 * rhs.x) + (lhs.m4 * rhs.y) + (lhs.m7 * rhs.z),
    //            (lhs.m2 * rhs.x) + (lhs.m5 * rhs.y) + (lhs.m8 * rhs.z),
    //            (lhs.m3 * rhs.x) + (lhs.m6 * rhs.y) + (lhs.m9 * rhs.z)
    //        );
    //    }
    //    public static Vector3 operator *(Vector3 lhs, float rhs)
    //    {
    //        return new Vector3(rhs * lhs.x, rhs * lhs.y, rhs * lhs.z);
    //    }
    //}
    //struct Vector4
    //{
    //    public float x, y, z, w;
    //    public Vector4(float xVal, float yVal, float zVal, float wVal)
    //    {
    //        x = xVal;
    //        y = yVal;
    //        z = zVal;
    //        w = wVal;
    //    }
    //    public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
    //    {
    //        return new Vector4
    //        (
    //            lhs.x + rhs.x,
    //            lhs.y + rhs.y,
    //            lhs.z + rhs.z,
    //            lhs.w + rhs.w
    //        );
    //    }
    //    public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
    //    {
    //        return new Vector4
    //        (
    //            lhs.x - rhs.x,
    //            lhs.y - rhs.y,
    //            lhs.z - rhs.z,
    //            lhs.w - rhs.w
    //        );
    //    }
    //    public static Vector4 operator *(Vector4 lhs, Vector4 rhs)
    //    {
    //        return new Vector4
    //        (
    //            lhs.x * rhs.x,
    //            lhs.y * rhs.y,
    //            lhs.z * rhs.z,
    //            lhs.w * rhs.w
    //        );
    //    }
    //    public static Vector4 operator *(Matrix4 lhs, Vector4 rhs)
    //    {
    //        return new Vector4
    //        (
    //            (lhs.m1 * rhs.x) + (lhs.m5 * rhs.y) + (lhs.m9 * rhs.z) + (lhs.m13 * rhs.w),
    //            (lhs.m2 * rhs.x) + (lhs.m6 * rhs.y) + (lhs.m10 * rhs.z) + (lhs.m14 * rhs.w),
    //            (lhs.m3 * rhs.x) + (lhs.m7 * rhs.y) + (lhs.m11 * rhs.z) + (lhs.m15 * rhs.w),
    //            (lhs.m4 * rhs.x) + (lhs.m8 * rhs.y) + (lhs.m12 * rhs.z) + (lhs.m16 * rhs.w)
    //        );
    //    }
    //    public static Vector4 operator *(Vector4 lhs, float rhs)
    //    {
    //        return new Vector4
    //        (
    //            lhs.x * rhs,
    //            lhs.y * rhs,
    //            lhs.z * rhs,
    //            lhs.w * rhs
    //        );
    //    }
    //    public float Magnitude()
    //    {
    //        return (float)Math.Sqrt((double)(x * x + y * y + z * z + w * w));
    //    }
    //    public void Normalize()
    //    {
    //        float m = Magnitude();
    //        this.x /= m;
    //        this.y /= m;
    //        this.z /= m;
    //        this.w /= m;
    //    }
    //    public float Dot(Vector4 rhs)
    //    {
    //        return x * rhs.x + y * rhs.y + z * rhs.z + w * rhs.w;
    //    }
    //    public Vector4 Cross(Vector4 rhs)
    //    {
    //        return new Vector4
    //        (
    //            y * rhs.z - z * rhs.y,
    //            z * rhs.x - x * rhs.z,
    //            x * rhs.y - y * rhs.x,
    //            0
    //        );
    //    }
    //}
}
