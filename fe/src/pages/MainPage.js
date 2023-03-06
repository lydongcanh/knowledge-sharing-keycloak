import React from "react";
import EnqueueDelayedJobForm from "../components/EnqueueDelayedJobForm";
import EnqueueFireAndForgetJobForm from "../components/EnqueueFireAndForgetJobForm";
import EnqueueRecurringJobForm from "../components/EnqueueRecurringJobForm";

export default function MainPage() {

    return (<div>
        <EnqueueFireAndForgetJobForm />
        <EnqueueDelayedJobForm />
        <EnqueueRecurringJobForm />
        <iframe
            src='https://localhost:7223/hangfire'
            allowFullScreen
            style={{
                height: '100vh',
                width: '100vw',
                boxSizing: 'border-box',
            }}
        />
    </div>);
};