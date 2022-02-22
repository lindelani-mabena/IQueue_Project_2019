package com.example.myapplication.data.model;

import com.example.myapplication.data.DateHelper;
import com.example.myapplication.data.Time;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Date;

public class Ticket {
    private int id;
    private String token;
    private String status;
    private boolean active;
    private String type;
    private Date initialDate;
    private Date lastUpdateDate;
    private Time estimatedWaitingTime;
    private Time startAt;
    private Time endAt;
    private Time duration;

    public Ticket(int id, String token, String status, boolean active, Date initialDate,
                  Time estimatedWaitingTime, Time startAt, Time endAt, Time duration) {
        this.id = id;
        this.token = token;
        this.status = status;
        this.active = active;
        this.initialDate = initialDate;
        this.estimatedWaitingTime = estimatedWaitingTime;
        this.startAt = startAt;
        this.endAt = endAt;
        this.duration = duration;
    }

    public Ticket(JSONObject jsonObject) {
        try {
            this.id = Integer.parseInt(jsonObject.getString("Id"));
            this.token = jsonObject.getString("Token");
            this.status = jsonObject.getString("Status");
            this.active = jsonObject.getBoolean("Active");
            this.type = jsonObject.getString("Type");
            String strInitialDate = jsonObject.getString("InitialDate");
            initialDate = DateHelper.convertToDateTime(strInitialDate);
            String strLastUpdate = jsonObject.getString("LastUpdate");
            this.lastUpdateDate = DateHelper.convertToDateTime(strLastUpdate);
            String strEstimatedWaitingTime = jsonObject.getString("EstimatedWaitingTime");
            this.estimatedWaitingTime = DateHelper.convertToTime(strEstimatedWaitingTime);
            String strStartAt = jsonObject.getString("StartAt");
            this.startAt = DateHelper.convertToTime(strStartAt);
            String strEndAt = jsonObject.getString("EndAt");
            this.endAt = DateHelper.convertToTime(strEndAt);
            String strDuration = jsonObject.getString("Duration");
            this.duration = DateHelper.convertToTime(strDuration);
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public boolean isActive() {
        return active;
    }

    public void setActive(boolean active) {
        this.active = active;
    }

    public Date getInitialDate() {
        return initialDate;
    }

    public void setInitialDate(Date initialDate) {
        this.initialDate = initialDate;
    }

    public Time getEstimatedWaitingTime() {
        return estimatedWaitingTime;
    }

    public void setEstimatedWaitingTime(Time estimatedWaitingTime) {
        this.estimatedWaitingTime = estimatedWaitingTime;
    }

    public Time getStartAt() {
        return startAt;
    }

    public void setStartAt(Time startAt) {
        this.startAt = startAt;
    }

    public Time getEndAt() {
        return endAt;
    }

    public void setEndAt(Time endAt) {
        this.endAt = endAt;
    }

    public Time getDuration() {
        return duration;
    }

    public void setDuration(Time duration) {
        this.duration = duration;
    }

    @Override
    public String toString() {
        return "Ticket{" +
                "id=" + id +
                ", token='" + token + '\'' +
                ", status='" + status + '\'' +
                ", active=" + active +
                ", initialDate=" + initialDate +
                ", estimatedWaitingTime=" + estimatedWaitingTime +
                ", startAt=" + startAt +
                ", EndAt=" + endAt +
                ", Duration=" + duration +
                '}';
    }
}
