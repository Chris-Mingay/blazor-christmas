<script>
	import { onMount } from 'svelte';
	import { get } from '$lib/req_utils.js';
	import { goto } from '$app/navigation';
	import Modal from '$lib/Modal.svelte';

	let memberships;

	onMount(getQuestions);

	async function getQuestions() {
		try {
			const json = await get(fetch, 'https://localhost:5021/api/leagues');
			console.log(json);
			if (json.succeeded) {
				memberships = json.data;
			}
		} catch (err) {
			console.log(err);
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

	function gotoLeague(membership){
		goto(`/leagues/${membership.leagueId}`);
	}

	function openLeagueOptions(){
		showLeagueOptionsModal = true;
	}

	function handleToggleModal(){
		showLeagueOptionsModal = !showLeagueOptionsModal;
	}

	let showLeagueOptionsModal = false;
	let leagueOptionsModal;

</script>


{#if memberships}
	<div class='bg-white rounded-2xl overflow-hidden text-gray-800 w-3/4 lg:w-108 mx-auto shadow-lg'>
		<div class='p-4 text-white bg-blue-500'>
			<h1 class='text-4xl font-bold'>Your Leagues</h1>
		</div>

		{#if memberships.length == 0}
			<div class='p-4'>
				<p>You haven't joined any leagues yet. :(</p>
			</div>
		{:else}
			<div class='grid grid-cols-8 divide-y'>
				{#each memberships as membership}
					<div class='col-span-5 pl-4 py-4 pr-2 font-semibold cursor-pointer' on:click={gotoLeague(membership)}>{membership.leagueName}</div>
					<div class='pl-2 pr-4 py-4 col-span-2'>
						<span class='inline-block font-semibold'>{membership.rank}{nth(membership.rank)}</span>
					</div>
					<div class='pl-2 pr-4 py-4'>{membership.points}pts</div>
				{/each}
			</div>
		{/if}

	</div>
{/if}

<button type='button' class='fixed bottom-5 right-5 w-20 h-20 bg-blue-500 text-white flex items-center justify-center rounded-full' on:click={openLeagueOptions}>
	<svg xmlns="http://www.w3.org/2000/svg" class="h-10 w-10" fill="none" viewBox="0 0 24 24" stroke="currentColor">
		<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 5v.01M12 12v.01M12 19v.01M12 6a1 1 0 110-2 1 1 0 010 2zm0 7a1 1 0 110-2 1 1 0 010 2zm0 7a1 1 0 110-2 1 1 0 010 2z" />
	</svg>
</button>

<!-- Open Links -->
<Modal
	title=""
	open={showLeagueOptionsModal}
	on:close={handleToggleModal}
	bind:this={leagueOptionsModal}
>
	<svelte:fragment slot="body">
		<div class='grid grid-cols divide-y'>
			<a href='/leagues/create' class='flex'>
				<div class='flex-1 p-4 font-semibold'>
					Create League
				</div>
				<div class='flex-0 p-4'>
					<svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
						<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v3m0 0v3m0-3h3m-3 0H9m12 0a9 9 0 11-18 0 9 9 0 0118 0z" />
					</svg>
				</div>
			</a>
			<a href='/leagues/join' class='flex'>
				<div class='flex-1 p-4 font-semibold'>
					Join League
				</div>
				<div class='flex-0 p-4'>
					<svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
						<path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M18 9v3m0 0v3m0-3h3m-3 0h-3m-2-5a4 4 0 11-8 0 4 4 0 018 0zM3 20a6 6 0 0112 0v1H3v-1z" />
					</svg>
				</div>
			</a>
		</div>
	</svelte:fragment>
</Modal>