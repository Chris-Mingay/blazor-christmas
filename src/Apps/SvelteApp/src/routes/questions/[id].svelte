<script>
	import { page } from '$app/stores';
	import { onMount } from 'svelte';
	import { get, post } from '$lib/req_utils.js';
	import Timer from '$lib/Timer.svelte';
	import QuestionOption from '$lib/QuestionOption.svelte';

	let id = $page.params.id;

	let questionPreview;
	let question;
	let answer;
	let correctAnswer;


	let questionRevealed = false;
	let answerSubmitted = false;
	let answerRevealed = false;
	let timer;
	let selectedOption;

	onMount(getQuestionPreview);

	async function getQuestionPreview(){
		try {
			const json = await get(fetch, `https://localhost:5021/api/Questions/preview/${id}`);
			if(json.succeeded){
				questionPreview = json.data;
			}
		} catch (err){
			console.log(err);
		}

	}

	async function revealQuestion() {
		try {
			const json = await get(fetch, `https://localhost:5021/api/Questions/${id}`);
			if (json.succeeded) {
				question = json.data.question;
				answer = json.data.answer;
				questionRevealed = true;
				timer.startTimer();
			}
		} catch (err) {
			console.log(err);
		}
	}

	async function revealAnswer(){

		console.log('revealAnswer');

		var data = {
			questionId: question.id,
			answerId: answer.id
		}
		if(selectedOption){
			data.questionOptionId = selectedOption.id
		}

		try {
			const json = await post(fetch, `https://localhost:5021/api/Questions/${id}`, data);
			if (json.succeeded) {
				const correctAnswerId = json.data;
				question.questionOptions.forEach(o => {
					if(o.id == correctAnswerId) correctAnswer = o;
				});
			}
		} catch (err) {
			console.log(err);
		}
	}

	function selectOption(option) {
		selectedOption = option.detail;
		timer.stopTimer();
		revealAnswer();
	}

	function categoryClass(question){
		switch(question.category){
			case "Sport":
				return "bg-yellow-500";
		}
	}

	const nth = function(d) {
		if (d > 3 && d < 21) return 'th';
		switch (d % 10) {
			case 1:  return "st";
			case 2:  return "nd";
			case 3:  return "rd";
			default: return "th";
		}
	}

</script>

{#if questionPreview}
<div class='bg-white rounded-2xl overflow-hidden text-gray-800 w-3/4 lg:w-108 mx-auto shadow-lg'>
	<div class='p-4 text-white {categoryClass(questionPreview)}'>
		<h1 class='text-4xl font-bold text-center'>{questionPreview.dayNumber}{nth(questionPreview.dayNumber)} - {questionPreview.category}</h1>
	</div>
	{#if !questionRevealed}
		<div class='p-4 bg-white'>
			<p class='mb-4'>When you're ready, click the button below to reveal todays mutliple choice question. You will have <b>30 seconds</b> to give your answer, good luck!</p>
			<p class='text-center'>
				<button type='button' class='inline-block px-6 py-3 text-white bg-blue-500 font-semibold rounded-lg text-xl' on:click={revealQuestion}>Reveal Question</button>
			</p>
		</div>

	{/if}
	<div  class:hidden={!questionRevealed}>
		<Timer bind:this={timer} on:timeUp={revealAnswer}/>
	</div>
	{#if questionRevealed}
		<div class='p-4 bg-white'>

			<p class='w-full font-semibold text-gray-600 mb-4 text-xl'>{question.text}</p>
			<div class='grid grid-cols-2 gap-4'>
				{#each question.questionOptions as option}
					<QuestionOption option={option}
													selected={selectedOption === option}
													correctId={correctAnswer?.id}
													on:clicked={selectOption}/>
				{/each}
			</div>

		</div>
	{/if}
</div>
{/if}

<span class='incorrect correct neutral'></span>