using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mono.Data.Sqlite;

public class SqliteManager
{
    private static SqliteManager instance;

    public static SqliteManager Instance {
        get
        {
            if (instance==null){
                instance = new SqliteManager();
            }
            return instance;
        }
    }

    //数据库连接类
    private SqliteConnection con;
    //数据库命令
    private SqliteCommand command;

    //打开数据库
    public void Open(string file) {
        con = new SqliteConnection("URI=file:" + file);
        con.Open();
    }

    public void Open()
    {
        con.Open();
    }
    //执行没有返回值的数据库命令
    public void executeNonQuery(string sqlStr) {
        command = new SqliteCommand(sqlStr, con);
        command.ExecuteNonQuery();
        command.Dispose();
    }

    //执行单个结果查询
    public int executeScalar(string sqlStr) {
        command = new SqliteCommand(sqlStr, con);
        object obj = command.ExecuteScalar();
        command.Dispose();
        return Convert.ToInt32(obj);
        
    }

    //执行多个结果的查询
    public SqliteDataReader executeQuery(string sqlStr) {
        command = new SqliteCommand(sqlStr, con);
        SqliteDataReader reader = command.ExecuteReader();
        command.Dispose();
        return reader;
    }

    public void Close() {
        con.Close();
    }
}
