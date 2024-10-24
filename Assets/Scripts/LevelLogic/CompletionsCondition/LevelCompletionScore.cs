using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelCompletionScore : LevelCondition
    {
        [SerializeField] private int m_Score; // ���������� �����
        public override bool IsCompleted // ��������� ��������� �� ������ ���������� ������
        {
            get
            {
                // ��������� ���� ������ �������� �������
                if (Player.Instance.ActiveShip == null) return false;
                
                // ���� �������� ����� ������ ������ ���������� ��a����� ��������� true
                if (Player.Instance.Score >= m_Score)
                {
                    return true;
                }
                
                return false;
            }
        }
    }
}

