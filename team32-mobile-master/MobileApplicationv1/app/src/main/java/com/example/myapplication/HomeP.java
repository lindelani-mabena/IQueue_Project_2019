package com.example.myapplication;

import androidx.appcompat.app.AppCompatActivity;
import androidx.cardview.widget.CardView;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;

public class HomeP extends AppCompatActivity implements View.OnClickListener {

    private CardView card1;
    private CardView card2;
    private CardView card3;
    private CardView card4;
    private String Access_Token;
    Members member;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home_p);

        card1 = (CardView) findViewById(R.id.card1);
        card2 = (CardView) findViewById(R.id.card2);
        card3 = (CardView) findViewById(R.id.card3);
        card4 = (CardView) findViewById(R.id.card4);

        SharedPreferences sharedpreferences = getSharedPreferences("userInfo", Context.MODE_PRIVATE);
        if (sharedpreferences.getLong("ExpiryDate", -1) > System.currentTimeMillis()) {
            Access_Token = sharedpreferences.getString("token", "");
        } else {
            SharedPreferences.Editor editor = sharedpreferences.edit();
          //  editor.clear();
         //   editor.apply();
        }



        card1.setOnClickListener(this);
        card2.setOnClickListener(this);
        card3.setOnClickListener(this);
        card4.setOnClickListener(this);

        member = new Members();
    }

    @Override
    public void onClick(View view) {
        Intent i;

        switch (view.getId()) {
            case R.id.card1:
                if(Access_Token== null || Access_Token=="")
                {
                    i = new Intent(getApplicationContext(),BranchesMapsActivity.class);
                    startActivity(i);
                    break;
                }
                else
                {
                    i = new Intent(HomeP.this,BranchesMapsActivity
                            .class);
                    startActivity(i);
                    break;
                }
            case R.id.card2:
                i = new Intent(HomeP.this, Update_Profile.class);
                startActivity(i);
                break;
            
            case R.id.card3:
                i = new Intent(HomeP.this, RateBranch.class);
                startActivity(i);
                break;
            
            case R.id.card4:
                Intent intent2 = new Intent(getApplicationContext(), QueueToken.class);
                startActivity(intent2);

                break;
        }
    }

}
