import React, { useState, useEffect } from "react";
import Slidebar from "../../components/member/Slidebar.jsx";
import authService from "../../services/authService";
import "../../styles/pages/BloodDonationFormPage.scss";
import MemberNavbar from "../../components/member/MemberNavbar.jsx";
import CheckInformation from "../../components/member/CheckInformation.jsx";
import TimeAndLocation from "../../components/member/TimeAndLocation.jsx";
import QuestionForm from "../../components/member/QuestionForm.jsx";

const BloodDonationFormPage = () => {
    const [step, setStep] = useState(0);
    const [infoValid, setInfoValid] = useState(false);
    const [showSuccess, setShowSuccess] = useState(false);

    // Questionnaire sub-steps (for step 2 - questionnaire)
    const [questionnaireStep, setQuestionnaireStep] = useState(0);
    const totalQuestionnaireSteps = 4; // We'll divide 9 questions into 4 pages

    // Dummy form state for step 1
    const [form, setForm] = useState({
        documentNumber: "",
        fullName: "",
        dob: "",
        gender: "",
        province: "",
        district: "",
        ward: "",
        address: "",
        email: "",
        phone: "",
    });
    const [errors, setErrors] = useState({});

    // Questionnaire answers state
    const [questionnaireAnswers, setQuestionnaireAnswers] = useState({});

    useEffect(() => {
        // Lấy thông tin từ user profile trước, sau đó từ localStorage
        const currentUser = authService.getCurrentUser();
        const userProfile = currentUser?.profile || {};
        const localInfo = JSON.parse(localStorage.getItem("memberInfo") || "{}");

        // Ưu tiên thông tin từ user profile
        const combinedInfo = {
            ...localInfo,
            ...userProfile,
            dob: userProfile.dateOfBirth || localInfo.dob || "",
            documentNumber: userProfile.documentNumber || localInfo.documentNumber || "",
            fullName: userProfile.fullName || localInfo.fullName || "",
            gender: userProfile.gender || localInfo.gender || "",
            province: userProfile.province || localInfo.province || "",
            district: userProfile.district || localInfo.district || "",
            ward: userProfile.ward || localInfo.ward || "",
            address: userProfile.address || localInfo.address || "",
            email: userProfile.email || localInfo.email || "",
            phone: userProfile.phone || localInfo.phone || "",
        };

        if (Object.keys(combinedInfo).length > 0) {
            setForm((f) => ({ ...f, ...combinedInfo }));
        }
    }, []);

    // Validate info step
    const validateInfo = () => {
        const newErrors = {};
        if (!form.documentNumber) newErrors.documentNumber = "Bắt buộc";
        if (!form.fullName) newErrors.fullName = "Bắt buộc";
        if (!form.dob) newErrors.dob = "Bắt buộc";
        if (!form.gender) newErrors.gender = "Bắt buộc";
        if (!form.province) newErrors.province = "Bắt buộc";
        if (!form.district) newErrors.district = "Bắt buộc";
        if (!form.ward) newErrors.ward = "Bắt buộc";
        if (!form.address) newErrors.address = "Bắt buộc";
        if (!form.email && !form.phone) {
            newErrors.email = "Bắt buộc 1 trong 2";
            newErrors.phone = "Bắt buộc 1 trong 2";
        }
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    // Update questionnaire answer
    const updateQuestionnaireAnswer = (questionName, value) => {
        setQuestionnaireAnswers((prev) => ({
            ...prev,
            [questionName]: value,
        }));
    };

    return (
        <>
            <MemberNavbar />
            <div className="donation-form-wrapper">
                <div className="donation-form-card">
                    <Slidebar step={step} infoValid={infoValid} />
                    <div className="donation-form-content">
                        {step === 0 && (
                            <CheckInformation
                                form={form}
                                setForm={setForm}
                                errors={errors}
                                validateInfo={validateInfo}
                                setInfoValid={setInfoValid}
                                setStep={setStep}
                            />
                        )}
                        {step === 1 && (
                            <TimeAndLocation setStep={setStep} />
                        )}
                        {step === 2 && (
                            <QuestionForm
                                questionnaireStep={questionnaireStep}
                                setQuestionnaireStep={setQuestionnaireStep}
                                questionnaireAnswers={questionnaireAnswers}
                                updateQuestionnaireAnswer={updateQuestionnaireAnswer}
                                totalQuestionnaireSteps={totalQuestionnaireSteps}
                                setStep={setStep}
                                showSuccess={showSuccess}
                                setShowSuccess={setShowSuccess}
                            />
                        )}
                    </div>
                </div>
            </div>
        </>
    );
};

export default BloodDonationFormPage;
