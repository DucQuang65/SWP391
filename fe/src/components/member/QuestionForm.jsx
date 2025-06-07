import React, { useState } from "react";

const QuestionForm = ({
    questionnaireStep,
    setQuestionnaireStep,
    questionnaireAnswers,
    updateQuestionnaireAnswer,
    totalQuestionnaireSteps,
    setStep,
    showSuccess,
    setShowSuccess
}) => {
    const [showFail, setShowFail] = useState(false);

    switch (questionnaireStep) {
        case 0:
            return (
                <div className="donation-form-step questionnaire-step">
                    <div className="questionnaire-header">
                        <h4>Phiếu khám sàng lọc người hiến máu tình nguyện</h4>
                        <p className="text-muted">Phần 1: Thông tin cơ bản</p>
                        <div className="progress-indicator">
                            <span className="current-step">1</span> / {totalQuestionnaireSteps}
                        </div>
                    </div>
                    {/* Câu hỏi 1 */}
                    <div className="question-block">
                        <div className="question-header">1. Anh/chị từng hiến máu chưa?</div>
                        <div className="question-body">
                            <div className="radio-group">
                                <label>
                                    <input
                                        type="radio"
                                        name="q1"
                                        value="yes"
                                        checked={questionnaireAnswers.q1 === 'yes'}
                                        onChange={(e) => updateQuestionnaireAnswer('q1', e.target.value)}
                                    /> Có
                                </label>
                                <label>
                                    <input
                                        type="radio"
                                        name="q1"
                                        value="no"
                                        checked={questionnaireAnswers.q1 === 'no'}
                                        onChange={(e) => updateQuestionnaireAnswer('q1', e.target.value)}
                                    /> Không
                                </label>
                            </div>
                        </div>
                    </div>
                    {/* Câu hỏi 2 */}
                    <div className="question-block">
                        <div className="question-header">2. Hiện tại, anh/chị có mắc bệnh lý nào không?</div>
                        <div className="question-body">
                            <div className="radio-group">
                                <label>
                                    <input
                                        type="radio"
                                        name="q2"
                                        value="yes"
                                        checked={questionnaireAnswers.q2 === 'yes'}
                                        onChange={(e) => updateQuestionnaireAnswer('q2', e.target.value)}
                                    /> Có
                                </label>
                                <label>
                                    <input
                                        type="radio"
                                        name="q2"
                                        value="no"
                                        checked={questionnaireAnswers.q2 === 'no'}
                                        onChange={(e) => updateQuestionnaireAnswer('q2', e.target.value)}
                                    /> Không
                                </label>
                            </div>
                            {questionnaireAnswers.q2 === 'yes' && (
                                <div className="question-detail">
                                    <label>Nếu có, xin ghi rõ:</label>
                                    <input
                                        type="text"
                                        className="form-control"
                                        placeholder="Ghi rõ bệnh lý..."
                                        value={questionnaireAnswers.q2_detail || ''}
                                        onChange={(e) => updateQuestionnaireAnswer('q2_detail', e.target.value)}
                                    />
                                </div>
                            )}
                        </div>
                    </div>
                    {/* Câu hỏi 3 */}
                    <div className="question-block">
                        <div className="question-header">3. Anh/chị đã từng mắc một trong các bệnh sau đây trước đây chưa:</div>
                        <div className="question-subtext">viêm gan B, viêm gan C, HIV, vảy nến, phì đại tiền liệt tuyến, sốc phản vệ, tai biến mạch máu não, nhồi máu cơ tim, lupus ban đỏ, động kinh, ung thư, hen suyễn, hoặc đã từng được cấy ghép mô/tạng?</div>
                        <div className="question-body">
                            <div className="radio-group">
                                <label>
                                    <input
                                        type="radio"
                                        name="q3"
                                        value="yes"
                                        checked={questionnaireAnswers.q3 === 'yes'}
                                        onChange={(e) => updateQuestionnaireAnswer('q3', e.target.value)}
                                    /> Có
                                </label>
                                <label>
                                    <input
                                        type="radio"
                                        name="q3"
                                        value="no"
                                        checked={questionnaireAnswers.q3 === 'no'}
                                        onChange={(e) => updateQuestionnaireAnswer('q3', e.target.value)}
                                    /> Không
                                </label>
                            </div>
                        </div>
                    </div>
                    <div className="form-actions">
                        <button
                            className="btn btn-outline-secondary"
                            onClick={e => { e.preventDefault(); setStep(1); }}
                        >
                            QUAY VỀ
                        </button>
                        <button
                            className="btn btn-danger"
                            onClick={e => { e.preventDefault(); setQuestionnaireStep(1); }}
                        >
                            TIẾP THEO
                        </button>
                    </div>
                </div>
            );
        case 1:
            return (
                <div className="donation-form-step questionnaire-step">
                    <div className="questionnaire-header">
                        <h4>Phiếu khám sàng lọc người hiến máu tình nguyện</h4>
                        <p className="text-muted">Phần 2: Tiền sử 12 tháng gần đây</p>
                        <div className="progress-indicator">
                            <span className="current-step">2</span> / {totalQuestionnaireSteps}
                        </div>
                    </div>
                    {/* Câu hỏi 4 */}
                    <div className="question-block">
                        <div className="question-header">4. Trong 12 tháng gần đây, anh/chị có:</div>
                        <div className="question-body">
                            <div className="sub-questions">
                                <div className="sub-question">
                                    <span>Khỏi bệnh sau khi mắc một trong các bệnh: sốt rét, giang mai, lao, viêm não-màng não, uốn ván, phẫu thuật ngoại khoa?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q4a"
                                                value="yes"
                                                checked={questionnaireAnswers.q4a === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q4a', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q4a"
                                                value="no"
                                                checked={questionnaireAnswers.q4a === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q4a', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Được truyền máu hoặc các chế phẩm máu?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q4b"
                                                value="yes"
                                                checked={questionnaireAnswers.q4b === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q4b', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q4b"
                                                value="no"
                                                checked={questionnaireAnswers.q4b === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q4b', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Tiêm Vacxin?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q4c"
                                                value="yes"
                                                checked={questionnaireAnswers.q4c === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q4c', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q4c"
                                                value="no"
                                                checked={questionnaireAnswers.q4c === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q4c', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                    {questionnaireAnswers.q4c === 'yes' && (
                                        <div className="question-detail">
                                            <label>Nếu có, xin ghi rõ tên vacxin:</label>
                                            <input
                                                type="text"
                                                className="form-control"
                                                placeholder="Tên vacxin..."
                                                value={questionnaireAnswers.q4c_detail || ''}
                                                onChange={(e) => updateQuestionnaireAnswer('q4c_detail', e.target.value)}
                                            />
                                        </div>
                                    )}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="form-actions">
                        <button
                            className="btn btn-outline-secondary"
                            onClick={(e) => { e.preventDefault(); setQuestionnaireStep(0); }}
                        >
                            QUAY VỀ
                        </button>
                        <button
                            className="btn btn-danger"
                            onClick={(e) => { e.preventDefault(); setQuestionnaireStep(2); }}
                        >
                            TIẾP THEO
                        </button>
                    </div>
                </div>
            );
        case 2:
            return (
                <div className="donation-form-step questionnaire-step">
                    <div className="questionnaire-header">
                        <h4>Phiếu khám sàng lọc người hiến máu tình nguyện</h4>
                        <p className="text-muted">Phần 3: Tiền sử 6 tháng gần đây</p>
                        <div className="progress-indicator">
                            <span className="current-step">3</span> / {totalQuestionnaireSteps}
                        </div>
                    </div>
                    {/* Câu hỏi 5 */}
                    <div className="question-block">
                        <div className="question-header">5. Trong 06 tháng gần đây, anh/chị có:</div>
                        <div className="question-body">
                            <div className="sub-questions">
                                <div className="sub-question">
                                    <span>Khỏi bệnh sau khi mắc một trong các bệnh: thương hàn, nhiễm trùng máu, bị rắn cắn, viêm tắc động mạch, viêm tắc tĩnh mạch, viêm tụy, viêm tủy xương?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5a"
                                                value="yes"
                                                checked={questionnaireAnswers.q5a === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5a', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5a"
                                                value="no"
                                                checked={questionnaireAnswers.q5a === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5a', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Sút cân nhanh không rõ nguyên nhân?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5b"
                                                value="yes"
                                                checked={questionnaireAnswers.q5b === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5b', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5b"
                                                value="no"
                                                checked={questionnaireAnswers.q5b === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5b', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Nổi hạch kéo dài?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5c"
                                                value="yes"
                                                checked={questionnaireAnswers.q5c === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5c', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5c"
                                                value="no"
                                                checked={questionnaireAnswers.q5c === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5c', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Thực hiện thủ thuật y tế xâm lấn (chữa răng, châm cứu, lăn kim, nội soi,…)?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5d"
                                                value="yes"
                                                checked={questionnaireAnswers.q5d === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5d', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5d"
                                                value="no"
                                                checked={questionnaireAnswers.q5d === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5d', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Xăm, xỏ lỗ tai, lỗ mũi hoặc các vị trí khác trên cơ thể?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5e"
                                                value="yes"
                                                checked={questionnaireAnswers.q5e === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5e', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5e"
                                                value="no"
                                                checked={questionnaireAnswers.q5e === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5e', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Sử dụng ma túy?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5f"
                                                value="yes"
                                                checked={questionnaireAnswers.q5f === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5f', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5f"
                                                value="no"
                                                checked={questionnaireAnswers.q5f === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5f', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Tiếp xúc trực tiếp với máu, dịch tiết của người khác hoặc bị thương bởi kim tiêm?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5g"
                                                value="yes"
                                                checked={questionnaireAnswers.q5g === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5g', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5g"
                                                value="no"
                                                checked={questionnaireAnswers.q5g === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5g', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Sinh sống chung với người nhiễm bệnh Viêm gan siêu vi B?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5h"
                                                value="yes"
                                                checked={questionnaireAnswers.q5h === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5h', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5h"
                                                value="no"
                                                checked={questionnaireAnswers.q5h === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5h', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Quan hệ tình dục với người nhiễm viêm gan siêu vi B, C, HIV, giang mai hoặc người có nguy cơ nhiễm các bệnh này?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5i"
                                                value="yes"
                                                checked={questionnaireAnswers.q5i === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5i', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5i"
                                                value="no"
                                                checked={questionnaireAnswers.q5i === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5i', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Quan hệ tình dục với người cùng giới?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5j"
                                                value="yes"
                                                checked={questionnaireAnswers.q5j === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5j', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q5j"
                                                value="no"
                                                checked={questionnaireAnswers.q5j === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q5j', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="form-actions">
                        <button
                            className="btn btn-outline-secondary"
                            onClick={(e) => { e.preventDefault(); setQuestionnaireStep(1); }}
                        >
                            QUAY VỀ
                        </button>
                        <button
                            className="btn btn-danger"
                            onClick={(e) => { e.preventDefault(); setQuestionnaireStep(3); }}
                        >
                            TIẾP THEO
                        </button>
                    </div>
                </div>
            );
        case 3:
            return (
                <div className="donation-form-step questionnaire-step">
                    <div className="questionnaire-header">
                        <h4>Phiếu khám sàng lọc người hiến máu tình nguyện</h4>
                        <p className="text-muted">Phần 4: Tiền sử gần đây & Câu hỏi đặc biệt</p>
                        <div className="progress-indicator">
                            <span className="current-step">4</span> / {totalQuestionnaireSteps}
                        </div>
                    </div>
                    {/* Câu hỏi 6 */}
                    <div className="question-block">
                        <div className="question-header">6. Trong 01 tháng gần đây, anh/chị có:</div>
                        <div className="question-body">
                            <div className="sub-questions">
                                <div className="sub-question">
                                    <span>Khỏi bệnh sau khi mắc bệnh viêm đường tiết niệu, viêm da nhiễm trùng, viêm phế quản, viêm phổi, sởi, ho gà, quai bị, sốt xuất huyết, kiết lỵ, tả, Rubella?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q6a"
                                                value="yes"
                                                checked={questionnaireAnswers.q6a === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q6a', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q6a"
                                                value="no"
                                                checked={questionnaireAnswers.q6a === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q6a', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Đi vào vùng có dịch bệnh lưu hành (sốt rét, sốt xuất huyết, Zika,…)?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q6b"
                                                value="yes"
                                                checked={questionnaireAnswers.q6b === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q6b', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q6b"
                                                value="no"
                                                checked={questionnaireAnswers.q6b === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q6b', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    {/* Câu hỏi 7 */}
                    <div className="question-block">
                        <div className="question-header">7. Trong 14 ngày gần đây, anh/chị có:</div>
                        <div className="question-body">
                            <div className="sub-questions">
                                <div className="sub-question">
                                    <span>Bị cúm, cảm lạnh, ho, nhức đầu, sốt, đau họng?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q7a"
                                                value="yes"
                                                checked={questionnaireAnswers.q7a === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q7a', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q7a"
                                                value="no"
                                                checked={questionnaireAnswers.q7a === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q7a', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    {/* Câu hỏi 8 */}
                    <div className="question-block">
                        <div className="question-header">8. Trong 07 ngày gần đây, anh/chị có:</div>
                        <div className="question-body">
                            <div className="sub-questions">
                                <div className="sub-question">
                                    <span>Dùng thuốc kháng sinh, kháng viêm, Aspirin, Corticoid?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q8a"
                                                value="yes"
                                                checked={questionnaireAnswers.q8a === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q8a', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q8a"
                                                value="no"
                                                checked={questionnaireAnswers.q8a === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q8a', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    {/* Câu hỏi 9 */}
                    <div className="question-block">
                        <div className="question-header">9. Câu hỏi dành cho phụ nữ:</div>
                        <div className="question-body">
                            <div className="sub-questions">
                                <div className="sub-question">
                                    <span>Hiện chị đang mang thai hoặc nuôi con dưới 12 tháng tuổi?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q9a"
                                                value="yes"
                                                checked={questionnaireAnswers.q9a === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q9a', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q9a"
                                                value="no"
                                                checked={questionnaireAnswers.q9a === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q9a', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                                <div className="sub-question">
                                    <span>Chấm dứt thai kỳ trong 12 tháng gần đây (sảy thai, phá thai, thai ngoài tử cung)?</span>
                                    <div className="radio-group">
                                        <label>
                                            <input
                                                type="radio"
                                                name="q9b"
                                                value="yes"
                                                checked={questionnaireAnswers.q9b === 'yes'}
                                                onChange={(e) => updateQuestionnaireAnswer('q9b', e.target.value)}
                                            /> Có
                                        </label>
                                        <label>
                                            <input
                                                type="radio"
                                                name="q9b"
                                                value="no"
                                                checked={questionnaireAnswers.q9b === 'no'}
                                                onChange={(e) => updateQuestionnaireAnswer('q9b', e.target.value)}
                                            /> Không
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="form-actions">
                        <button
                            className="btn btn-outline-secondary"
                            onClick={(e) => { e.preventDefault(); setQuestionnaireStep(2); }}
                        >
                            QUAY VỀ
                        </button>
                        <button
                            className="btn btn-danger"
                            onClick={(e) => {
                                e.preventDefault();
                                // Chỉ kiểm tra các câu trả lời loại trừ, bỏ qua q1 và q2_detail
                                const hasYes = Object.keys(questionnaireAnswers).some(
                                    (key) => key !== 'q1' && key !== 'q2_detail' && questionnaireAnswers[key] === 'yes'
                                );
                                if (hasYes) {
                                    setShowFail(true);
                                    setShowSuccess(false);
                                } else {
                                    setShowSuccess(true);
                                    setShowFail(false);
                                }
                            }}
                        >
                            XÁC NHẬN
                        </button>
                    </div>
                    {showSuccess && (
                        <div className="success-notification">
                            <i className="bi bi-check-circle-fill"></i>
                            ĐÃ NHẬN ĐƯỢC ĐƠN ĐĂNG KÝ CỦA BẠN, THEO DÕI THANH THÔNG BÁO Ở TRANG CHỦ ĐỂ BIẾT THÊM THÔNG TIN
                        </div>
                    )}
                    {showFail && (
                        <div className="fail-notification">
                            <i className="bi bi-x-circle-fill"></i>
                            ĐĂNG KÝ HIẾN MÁU KHÔNG THÀNH CÔNG DO KHÔNG ĐỦ ĐIỀU KIỆN. VUI LÒNG KIỂM TRA LẠI CÁC CÂU TRẢ LỜI!
                        </div>
                    )}
                </div>
            );
        default:
            return null;
    }
};

export default QuestionForm;
