import axios from 'axios';

const axiosInstance = axios.create({ baseURL: 'https://localhost:7223' });

export async function EnqueueFireAndForgetJobAsync(message) {
    await axiosInstance.post(`/hangfire-jobs/fire-and-forget-job?message=${message}`);
}

export async function EnqueueDelayedJobAsync(message, delaySeconds) {
    await axiosInstance.post(`/hangfire-jobs/delayed-job?message=${message}&delaySeconds=${delaySeconds}`);
}

export async function EnqueueRecurringJobAsync(jobId, message, cronExpression) {
    await axiosInstance.post(`/hangfire-jobs/recurring-job?jobId=${jobId}&message=${message}&cronExpression=${cronExpression}`);
}