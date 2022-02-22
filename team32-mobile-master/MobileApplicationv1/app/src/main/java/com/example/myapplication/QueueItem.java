package com.example.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

public class QueueItem extends ArrayAdapter<String> {

    private final Activity context;
    private final String[] QToken;
    private final String[] QStatus;


    public QueueItem(Activity context, String[] QToken, String[] QStatus) {
        super(context, R.layout.activity_queue_list, QToken);
        // TODO Auto-generated constructor stub

        this.context = context;
        this.QStatus = QStatus;
        this.QToken = QToken;

    }


    public View getView(int position, View view, ViewGroup parent) {
        LayoutInflater inflater = context.getLayoutInflater();
        View rowView = inflater.inflate(R.layout.activity_queue_list, null, true);

        TextView nameText = (TextView) rowView.findViewById(R.id.token);
        nameText.setText(QToken[position]);

        TextView DescriptionText = (TextView) rowView.findViewById(R.id.status);


        DescriptionText.setText(QStatus[position]);


        return rowView;

    };



}


