using BlazorSportsDataWebAsm.Shared.Models;
using ShimmerRT.models;
using System;

namespace Models
{
    [Serializable]
    public class BaseUnityModel
    {
        public int id;
        public int Id => id;

        public string createdBy;
        public string CreatedBy => createdBy;

        public string modifiedBy;
        public string ModifiedBy => createdBy;

        public string createdAt;
        public DateTime CreatedAt => Convert.ToDateTime(createdAt);
        public string modifiedAt;
        public DateTime ModifiedAt => Convert.ToDateTime(modifiedAt);

    }

    [Serializable]
    public class UnityPlayerModel : BaseUnityModel
    {
        public string email;
        public string Email => email;
        public string firstName;
        public string FirstName => firstName;
        public string lastName;
        public string LastName => lastName;

        public string phone;
        public string Phone => phone;
        public int age;
        public int Age => age;
        public double weight;
        public double Weight => weight;
        public double height;
        public double Height => height;

        public int teamID;
        public int TeamID => teamID;

        //public string team;
        //public string trainingRecords;

        public string teamKey;

        public override string ToString()
        {
            return $"ID: {Id} - {firstName} - {createdAt} - {CreatedAt}";
        }
    }

    [Serializable]
    public class UnityTrainingRecord : BaseUnityModel { }

    [Serializable]
    public class UnityShimmerDataModel
    {
        //public int id;
        //public int Id => id;

        public UnityShimmerDataModel(ShimmerDataModel s)
        {
            this.t = s.T;
            this.lN_X = s.LN_X;
            this.lN_Y = s.LN_Y;
            this.lN_Z = s.LN_Z;
            this.g_X = s.G_X;
            this.g_Y = s.G_Y;
            this.g_Z = s.G_Z;
            this.m_X = s.M_X;
            this.m_Y = s.M_Y;
            this.m_Z = s.M_Z;
            this.a_A = s.A_A;
            this.a_X = s.A_X;
            this.a_Y = s.A_Y;
            this.a_Z = s.A_Z;
            this.q_0 = s.Q_0;
            this.q_1 = s.Q_1;
            this.q_2 = s.Q_2;
            this.q_3 = s.Q_3;
        }

        public float t;
        public float T => t;

        public float lN_X;
        public float LN_X => lN_X;
        public float lN_Y;
        public float LN_Y => lN_Y;
        public float lN_Z;
        public float LN_Z => lN_Z;

        public float g_X;
        public float G_X => g_X;
        public float g_Y;
        public float G_Y => g_Y;
        public float g_Z;
        public float G_Z => g_Z;

        public float m_X;
        public float M_X => m_X;
        public float m_Y;
        public float M_Y => m_Y;
        public float m_Z;
        public float M_Z => m_Z;

        public float a_A;
        public float A_A => a_A;

        public float a_X;
        public float A_X => a_X;

        public float a_Y;
        public float A_Y => a_Y;
        public float a_Z;
        public float A_Z => a_Z;

        public float q_0;
        public float Q_0 => q_0;
        public float q_1;
        public float Q_1 => q_1;
        public float q_2;
        public float Q_2 => q_2;
        public float q_3;
        public float Q_3 => q_3;

    }
}
