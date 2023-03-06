import { useToast, FormControl, FormLabel, Input, FormErrorMessage, Button, HStack } from '@chakra-ui/react';
import { Field, Form, Formik } from 'formik';
import { EnqueueFireAndForgetJobAsync } from '../helpers/ProcessingServiceApis';

export default function EnqueueFireAndForgetJobForm() {
    const toast = useToast();

    return (
        <div style={{ margin: 16 }}>
            <Formik
                initialValues={{ message: '' }}
                onSubmit={async (values, actions) => {
                    try {
                        await EnqueueFireAndForgetJobAsync(values.message);
                        toast({
                            title: 'Fire & forget job queued',
                            description: `Data: ${JSON.stringify(values)}`,
                            status: 'success',
                            duration: 9000,
                            isClosable: true,
                        });
                    } catch(e) {
                        toast({
                            title: 'Fire & forget job error',
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
                    <Form title='Fire and forget jobs'>
                        <HStack align={'end'}>
                            <Field name='message'>
                                {({ field, form }) => (
                                    <FormControl isInvalid={form.errors.message && form.touched.message}>
                                        <FormLabel>Fire and forget job</FormLabel>
                                        <Input {...field} placeholder='message' />
                                        <FormErrorMessage>{form.errors.message}</FormErrorMessage>
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