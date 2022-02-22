package com.example.myapplication;


import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class NetworkClient {


    public static Retrofit retrofitInstance =null;

    public  static Retrofit getNetworkClient()
    {
        retrofitInstance = new Retrofit.Builder()
                .baseUrl("http://192.168.137.1:1997/")
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        return retrofitInstance;
    }
}