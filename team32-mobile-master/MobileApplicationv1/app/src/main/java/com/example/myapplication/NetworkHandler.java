package com.example.myapplication;

import org.json.JSONObject;

import java.util.List;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.Headers;
import retrofit2.http.POST;
import retrofit2.http.Path;
import retrofit2.http.Query;

public interface NetworkHandler {
    @GET("api/clients")
    Call<List<Client>> getUsers();
    @GET("api/branches")
    Call<List<Branch>> getBranches();
    @POST("api/Account/Register/")
    Call<Client> Register(@Body Client body);
    @POST("api/Account/Profile/Update")
    @Headers({"Content-Type: application/x-www-form-urlencoded;"})
    @GET("token")
    Call<Void> Login(@Query("Username")  String username,
                     @Query ("Password")  String password,
                     @Query ("grant_type") String granttype);

    @GET("api/Branches/{id}")
    Call<Branch> getBranch(@Path("id") String id);
    @GET("api/Services/{id}")
    Call<Service> getService(@Path("id") int id);
    @GET("api/Services")
    Call<List<Service>> getServices(@Query("Branch_Id") String Branch_Id);
    @POST("api/Tickets")
    Call<Ticket> Book(@Body Ticket body);
    @POST("api/Services/{id}/Join/Branches/{branchid}/mobile")
    Call<Ticket> joinQueue(@Path("id") String id, @Path("branchid") String branchid, @Header("Authorization") String auth);
}
