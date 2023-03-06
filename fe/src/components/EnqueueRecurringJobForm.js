import { useToast, FormControl, FormLabel, Input, FormErrorMessage, Button, HStack, NumberInput, NumberInputField, NumberInputStepper, NumberIncrementStepper, NumberDecrementStepper } from '@chakra-ui/react';
import { Field, Form, Formik } from 'formik';
import { EnqueueRecurringJobAsync } from '../helpers/ProcessingServiceApis';

export default function EnqueueRecurringJobForm() {
    const toast = useToast();

    return (
        <div style={{ margin: 16 }}>
            <Formik
                initialValues={{ message: '', jobId: '', cronExpression: '' }}
                onSubmit={async (values, actions) => {
                    try {
                        await EnqueueRecurringJobAsync(values.jobId, values.message, values.cronExpression);
                        toast({
                            title: 'Recurring job queued',
                            description: `Data: ${JSON.stringify(values)}`,
                            status: 'success',
                            duration: 9000,
                            isClosable: true,
                        });
                    } catch(e) {
                        toast({
                            title: 'Recurring job error',
                            description: `Error: ${JSON.stringify(e)}`,
                            status: 'error',
                            duration: 9000,
                            isClosable: true,
                        });
                        console.error(e);
                    } finally {
                        actions.setSubmitting(false);
                    }
                }}
            >
                {(props) => (
                    <Form title='Recurring jobs'>
                        <HStack justifyContent={'end'} align={'end'}>
                            <Field name='jobId'>
                                {({ field, form }) => (
                                    <FormControl isInvalid={form.errors.jobId && form.touched.jobId}>
                                        <FormLabel>Recurring jobs</FormLabel>
                                        <Input {...field} placeholder='job name' />
                                        <FormErrorMessage>{form.errors.jobId}</FormErrorMessage>
                                    </FormControl>
                                )}
                            </Field>
                            <Field name='message'>
                                {({ field, form }) => (
                                    <FormControl isInvalid={form.errors.message && form.touched.message}>
                                        <Input {...field} placeholder='message' />
                                        <FormErrorMessage>{form.errors.message}</FormErrorMessage>
                                    </FormControl>
                                )}
                            </Field>
                            <Field name='cronExpression'>
                                {({ field, form }) => (
                                    <FormControl isInvalid={form.errors.cronExpression && form.touched.cronExpression}>
                                        <Input {...field} placeholder='cron expression' />
                                        <FormErrorMessage>{form.errors.cronExpression}</FormErrorMessage>
                                    </FormControl>
                                )}
                            </Field>
                            <Button
                                width={30}
                                colorScheme='teal'
                                isLoading={props.isSubmitting}
                                type='submit'
                            >
                                +
                            </Button>
                        </HStack>
                    </Form>
                )}
            </Formik>
        </div>
    )
}