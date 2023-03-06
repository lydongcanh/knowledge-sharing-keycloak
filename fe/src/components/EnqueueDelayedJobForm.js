import { useToast, FormControl, FormLabel, Input, FormErrorMessage, Button, HStack, NumberInput, NumberInputField, NumberInputStepper, NumberIncrementStepper, NumberDecrementStepper } from '@chakra-ui/react';
import { Field, Form, Formik } from 'formik';
import { EnqueueDelayedJobAsync } from '../helpers/ProcessingServiceApis';

export default function EnqueueDelayedJobForm() {
    const toast = useToast();

    return (
        <div style={{ margin: 16 }}>
            <Formik
                initialValues={{ message: '', delaySeconds: 0 }}
                onSubmit={async (values, actions) => {
                    try {
                        await EnqueueDelayedJobAsync(values.message, values.delaySeconds);
                        toast({
                            title: 'Delayed job queued',
                            description: `Data: ${JSON.stringify(values)}`,
                            status: 'success',
                            duration: 9000,
                            isClosable: true,
                        });
                    } catch(e) {
                        toast({
                            title: 'Delayed job error',
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
                    <Form title='Delayed jobs'>
                        <HStack justifyContent={'space-between'} align={'end'}>
                            <Field name='message'>
                                {({ field, form }) => (
                                    <FormControl isInvalid={form.errors.message && form.touched.message}>
                                        <FormLabel>Delayed jobs</FormLabel>
                                        <Input {...field} placeholder='message' />
                                        <FormErrorMessage>{form.errors.message}</FormErrorMessage>
                                    </FormControl>
                                )}
                            </Field>
                            <Field name='delaySeconds'>
                                {({ field, form }) => (
                                    <FormControl isInvalid={form.errors.delaySeconds && form.touched.delaySeconds}>
                                        <Input {...field} placeholder='delay seconds' />
                                        <FormErrorMessage>{form.errors.delaySeconds}</FormErrorMessage>
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