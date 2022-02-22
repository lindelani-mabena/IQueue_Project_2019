package com.example.myapplication;



import java.security.PrivateKey;
import java.sql.Time;


public class Members {
    private static String branchName;
    private static  String serviceName;
    private static Boolean Joined;
    private static  Boolean loggedIn;
    private static String token;
    private static String Access_Token;

    private static String EstimatedWaitingTime;

    private static int branchID;
    private static int ServeId;


    public  String getEstimatedEaitingTime() {
        return EstimatedWaitingTime;
    }

    public  void setEstimatedEaitingTime(String estimatedEaitingTime) {
        EstimatedWaitingTime = estimatedEaitingTime;
    }

    public Members(){

    }
    public Members(String branchName, String serviceName, Boolean joined, String t) {
        this.branchName = branchName;
        this.serviceName = serviceName;
        this.Joined = joined;
        this.token = t;

    }

    public  int getServeId() {
        return ServeId;
    }

    public  void setServeId(int serveId) {
        ServeId = serveId;
    }

    public int getBranchID() {
        return branchID;
    }

    public void setBranchID(int branchID) {
        Members.branchID = branchID;
    }

    public  String getToken() {
        return token;
    }

    public  void setToken(String token) {
        Members.token = token;
    }

    public String getBranchName() {
        return branchName;
    }

    public void setBranchName(String branchName) {
        this.branchName = branchName;
    }

    public String getServiceName() {
        return serviceName;
    }

    public  void setServiceName(String serviceName) {
        Members.serviceName = serviceName;
    }

    public  Boolean getJoined() {
        return Joined;
    }

    public  void setJoined(Boolean joined) {
        Joined = joined;
    }

    public  Boolean getLoggedIn() {
        return loggedIn;
    }

    public  void setLoggedIn(Boolean loggedIn) {
        Members.loggedIn = loggedIn;
    }
    public  String getAccess_Token() {
        return Access_Token;
    }

    public  void setAccess_Token(String access_Token) {
        Access_Token = access_Token;
    }
}
